using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Models.Entities;
using Models.Enums;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;
using Type = Models.Enums.Type;

namespace Services.Services;

public class ItemService : CRUDRepository<Item, ItemSearchObject, ItemCreateUpdateRestDto, ItemCreateUpdateRestDto>,
    IItemService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;

    public ItemService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    static object isLocked = new object();
    static MLContext mlContext = null;
    static ITransformer model = null;

    public List<Item> Recommend(int userId)
    {
        var all = _db.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Item).ToList();
        List<int> allproducts = new List<int>();
        foreach (var item in all)
        {
            foreach (var item2 in item.OrderDetails)
            {
                allproducts.Add(item2.ItemId);
            }
        }

        if (allproducts.Distinct().Count() < 2)
            return new List<Item>();


        var orderProducts = _db.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Item)
            .Where(x => x.UserId == userId).ToList();
        if (orderProducts.Count == 0)
            return new List<Item>();
        int id;
        List<int> products = new List<int>();
        foreach (var item in orderProducts)
        {
            foreach (var item2 in item.OrderDetails)
            {
                products.Add(item2.ItemId);
            }
        }

        if (products.Distinct().Count() >= 2)
        {
            var list = products.Distinct();
            Random rand = new Random();
            int r = rand.Next(list.Count() - 1);
            id = products.ElementAt(r);
        }
        else
        {
            id = products.ElementAt(0);
        }

        lock (isLocked)
        {
            if (mlContext == null)
            {
                mlContext = new MLContext();

                var tmpData = _db.Orders.Include("OrderDetails").ToList();

                var data = new List<ProductEntry>();

                foreach (var x in tmpData)
                {
                    if (x.OrderDetails.Count > 1)
                    {
                        var distinctItemId = x.OrderDetails.Select(y => y.ItemId).ToList();

                        distinctItemId.ForEach(y =>
                        {
                            var relatedItems = x.OrderDetails.Where(z => z.ItemId != y);

                            foreach (var z in relatedItems)
                            {
                                data.Add(new ProductEntry()
                                {
                                    ProductID = (uint)y,
                                    CoPurchaseProductID = (uint)z.ItemId,
                                });
                            }
                        });
                    }
                }


                var traindata = mlContext.Data.LoadFromEnumerable(data);


                MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
                options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
                options.LabelColumnName = "Label";
                options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                options.Alpha = 0.01;
                options.Lambda = 0.025;
                options.NumberOfIterations = 100;
                options.C = 0.00001;


                var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                model = est.Fit(traindata);
            }
        }


        List<Item> allItems = _db.Item.ToList();

        var predictionResult = new List<Tuple<Item, float>>();

        foreach (var item in allItems)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
            var prediction = predictionEngine.Predict(new ProductEntry()
            {
                ProductID = (uint)id,
                CoPurchaseProductID = (uint)item.Id
            });

            predictionResult.Add(new Tuple<Item, float>(item, prediction.Score));
        }

        var finalResult = predictionResult.OrderByDescending(x => x.Item2)
            .Select(x => x.Item1).ToList();

        return _mapper.Map<List<Item>>(finalResult);
    }

    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }

    public class ProductEntry
    {
        [KeyType(count: 10)] public uint ProductID { get; set; }

        [KeyType(count: 10)] public uint CoPurchaseProductID { get; set; }

        public float Label { get; set; }
    }

    public override IQueryable<Item> AddFilter(IQueryable<Item> entity, ItemSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.Name))
        {
            entity = entity.Where(x => x.Name.ToLower().Contains(obj.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(obj.Type))
        {
            var type = Enum.Parse<Type>(obj.Type, ignoreCase: true);
            entity = entity.Where(x => x.Type == type);
        }

        if (!string.IsNullOrWhiteSpace(obj.Brand))
        {
            var brand = Enum.Parse<Brand>(obj.Brand, ignoreCase: true);
            entity = entity.Where(x => x.Brand == brand);
        }

        if (!string.IsNullOrWhiteSpace(obj.Availability))
        {
            var availability = Enum.Parse<Availability>(obj.Availability, ignoreCase: true);
            entity = entity.Where(x => x.Availability == availability);
        }

        return entity;
    }
}