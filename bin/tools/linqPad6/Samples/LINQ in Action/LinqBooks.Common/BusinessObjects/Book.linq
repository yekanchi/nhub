<Query Kind="Program" />

// This document shows how a class used in the LINQ in Action samples is declared
// This code does not compile as is

// namespace LinqInAction.LinqBooks.Common

public class Book
{
  public IEnumerable<Author> Authors {get; set;}
  public String Isbn {get; set;}
  public String Notes {get; set;}
  public Int32 PageCount {get; set;}
  public Decimal Price {get; set;}
  public DateTime PublicationDate {get; set;}
  public Publisher Publisher {get; set;}
  public IEnumerable<Review> Reviews {get; set;}
  public Subject Subject {get; set;}
  public String Summary {get; set;}
  public String Title {get; set;}

  public override String ToString()
  {
    return Title;
  }
}