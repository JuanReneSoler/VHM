// See https://aka.ms/new-console-template for more information
using Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

var stringConnection = "Server=10.0.0.9;Database=VHM;User Id=sa;Password=Masterxp01;Trusted_Connection=True;Integrated Security=False";
var optionBuilder = new DbContextOptionsBuilder<VHMContext>();
optionBuilder.UseSqlServer(stringConnection);



Func<VHMContext, int> createAndList = new Func<VHMContext, int>((VHMContext _context) =>
{

    Console.WriteLine("Insertando un producto");
    _context.Products.Add(new Product
    {
        Name = "Producto de Prueba",
        PriceUnit = 2.0M,
        ProveedorId = 1,
        TypeId = 1,
        Description = "Esto es una prueba",
    });
    _context.SaveChanges();


    foreach (var product in _context.Products.ToList()) Console.WriteLine($@"* Id: {product.Id}, Name: {product.Name}\n");
    return 0;
});

Func<VHMContext, int> updateAndList = new Func<VHMContext, int>((VHMContext _context) =>
    {
        Console.WriteLine("Valores antes del Update");
        foreach (var product in _context.Products.ToList()) Console.WriteLine($@"* Id: {product.Id}, Name: {product.Name}\n");
        Console.WriteLine("Updating...");
        var p = _context.Products.First(x => x.Id == 1);
        p.Name = "Nombre actualizado";
        _context.Update(p);
        _context.SaveChanges();
        foreach (var product in _context.Products.ToList()) Console.WriteLine($@"* Id: {product.Id}, Name: {product.Name}\n");
        return 0;
    });

Func<VHMContext, int> deleteAndList = new Func<VHMContext, int>((VHMContext _context) =>
{
    Console.WriteLine("Valores antes del delete");
    foreach (var product in _context.Products.ToList()) Console.WriteLine($@"* Id: {product.Id}, Name: {product.Name}\n");
    Console.WriteLine("Deleting...");
    var p = _context.Products.First(x => x.Id == 1);
    _context.Remove(p);
    _context.SaveChanges();
    foreach (var product in _context.Products.ToList()) Console.WriteLine($@"* Id: {product.Id}, Name: {product.Name}\n");
    return 0;
});

Func<VHMContext, int> listOfItems = new Func<VHMContext, int>((VHMContext _context) =>
    {
        Console.WriteLine("Lista de elementos");
        foreach (var product in _context.Products.ToList()) Console.WriteLine($@"Id: {product.Id}, Name: {product.Name}");
	return 0;
    });

using (var _context = new VHMContext(optionBuilder.Options))
{
    //createAndList(_context);
    //updateAndList(_context);
    //deleteAndList(_context);
    listOfItems(_context);
}








