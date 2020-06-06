<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

// This document shows how a class used in the LINQ in Action samples is declared
// This code does not compile as is

// namespace LinqInAction.LinqBooks.Common

public class Publisher
{
  public String Name {get; set;}
  public Bitmap Logo {get; set;}
  public String WebSite {get; set;}

  public override string ToString()
  {
    return Name;
  }
}