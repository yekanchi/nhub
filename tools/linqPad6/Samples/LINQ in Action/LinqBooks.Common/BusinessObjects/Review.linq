<Query Kind="Program" />

// This document shows how a class used in the LINQ in Action samples is declared
// This code does not compile as is

// namespace LinqInAction.LinqBooks.Common

public class Review
{
  public Book Book {get; set;}
  //public Guid User {get; set;}
  public User User {get; set;}
  public int Rating {get; set;}
  public String Comments {get; set;}
}