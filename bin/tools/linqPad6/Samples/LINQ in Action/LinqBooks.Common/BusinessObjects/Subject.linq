<Query Kind="Program" />

// This document shows how a class used in the LINQ in Action samples is declared
// This code does not compile as is

// namespace LinqInAction.LinqBooks.Common

public class Subject
{
  public String Description {get; set;}
  public String Name {get; set;}

  public override string ToString()
  {
    return Name;
  }
}