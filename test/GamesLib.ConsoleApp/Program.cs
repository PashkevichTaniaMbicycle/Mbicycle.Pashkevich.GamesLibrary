using System;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories;
using GamesLib.DataAccess.UnitOfWork;

using var context = new GamesLibContext();

var unitOfWork = new UnitOfWork(context);
var gameRepo = new GameRepository(context);
var devRepo = new DevRepository(context);
var publisherRepo = new PublisherRepository(context);

try
{
    await unitOfWork.BeginTransactionAsync();
    
    var dev = new Dev { Title = "Naughty Dog" };
    var publisher = new Publisher { Title = "Sony" };


    await devRepo.AddAsync(dev);
    await publisherRepo.AddAsync(publisher);
    
    Console.WriteLine(dev.Id);
    Console.WriteLine(publisher.Id);

    await gameRepo.AddAsync(new Game { Title = "The Last Of Us Part I",  Description = "something", ReleaseDate = DateTime.Now, Publisher = publisher, Dev = dev });

    await unitOfWork.CommitTransactionAsync();
}
catch
{
    await unitOfWork.RollbackTransactionAsync();
}