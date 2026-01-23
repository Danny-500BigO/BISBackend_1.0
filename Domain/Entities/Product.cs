using System;
// using System.Collections.Generic;
namespace BakeryApi.Domain.Entities
{
public class Product
    {

public int Product_Id { get; set;}
public DateTime Introduce_Date { get; set;}
public DateTime Expiry_Date { get; set;}
public DateTime Manufactured_Date { get; set;}
public int Price { get; set;}
public int Weight { get; set;}
public string Product_Name { get; set;}

    }

}