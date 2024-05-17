using DesignPattern_Adapter.Adapters;
using DesignPattern_Adapter.Adapters.Adaptee;

IDBStudentAccess dbStudentAccessAdapter = new StudentFileAccessAdapter("Students.xml");
IDBStudentAccess dbStudentAccess = new DBStudentAccess();

Console.WriteLine(dbStudentAccess.GetStudent(5));
Console.WriteLine(dbStudentAccessAdapter.GetStudent(5));