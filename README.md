# ClearPagination

Just package for paginate any IEnumerable results.

How to use:

    var model = db.ApplicationUsers.Select(x => new
    {
        x.Id,
        CompanyName = x.Company.Name
    })
    .Paginate(page: 5, pageSize: 20);
    // or .Paginate(5);
    // or .Paginate(new Pagination(5));
    // or .Paginate(new Pagination(5, 20));

    // Paginate() method performs Count() before Skip() and Take()
    Console.WriteLine(model.Pagination.SelectedRows); // e.g. 253 (same as db.ApplicationUsers.Count())
    Console.WriteLine(model.Pagination.RowsFrom); // 81
    Console.WriteLine(model.Pagination.RowsTo); // 100
    Console.WriteLine(model.List.Count()); // 20 pageSize
    // model.List is IEnumerable
    foreach (var user in model.List)
    {
        Console.WriteLine(user.CompanyName);
    }

Example of Cast() method, useful in Entity Framework:

    Func<int, string> MakeUrlForAnswer = x => $"Url for: {x}";
    var imagesPath = @"c:\work\pictures";
    var db = container.Resolve<AppDatabaseContext>();
    var res = db.Answers.Select(x => new
    {
        // only sql-translatable expressions
        x.Id,
        x.QuestionId,
        x.Text
    })
    .Paginate(5, pageSize: 3)
    .Cast(x => new
    {
        // any functions can be used, so you can prepare data for use
        Url = MakeUrlForAnswer(x.Id), 
        Path = Path.Combine(imagesPath, $"{x.Id}.jpg")
    });

    foreach (var item in res.List)
    {
        Console.WriteLine(item.Url);
        Console.WriteLine(item.Path);
    }
    // Outputs:
    // Url for: 57
    // c:\work\pictures\57.jpg
    // Url for: 56
    // c:\work\pictures\56.jpg
    // Url for: 55
    // c:\work\pictures\55.jpg

