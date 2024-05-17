using DesignPattern_Factory.Factories;
using DesignPattern_Factory.Utility;

FileSeeder fileSeeder = new();

fileSeeder.XmlUserSeed();

BaseFactoryMethod fileFactory = new FileFactory();

var student = fileFactory.GetUser(1);
var admin = fileFactory.GetUser(7);
var teacher = fileFactory.GetUser(9);

Console.WriteLine(student.GetInfo());
Console.WriteLine(admin.GetInfo());
Console.WriteLine(teacher.GetInfo());