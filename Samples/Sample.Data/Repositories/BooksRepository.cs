﻿using System;
using System.Data.Entity;
using Joker.EntityFramework.Repositories;
using Sample.Data.Context;
using Sample.Domain.Models;

namespace Sample.Data.Repositories
{
  public class BooksRepository : Repository<Book>
  {
    private readonly ISampleDbContext context;

    public BooksRepository(ISampleDbContext context) 
      : base(context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected override IDbSet<Book> DbSet => context.Books;
  }
}