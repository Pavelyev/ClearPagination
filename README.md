# ClearPagination

Just package for paginate any IEnumerable results. Useful for EF.

    Func<int, string> MakeUrlForAnswer = x => $"Url for: {x}";
    var imagesPath = @"c:\work\pictures";
    var db = container.Resolve<AppDatabaseContext>();
    var page = 5;
    var res = db.Answers.Select(x => new
    {
        // only sql-translatable expressions
        x.Id,
        x.QuestionId,
        x.Text
    })
    .Paginate(page, pageSize: 3)
    .Cast(x => new
    {
        // any functions can be used, so you can prepare data for use
        Url = MakeUrlForAnswer(x.Id), 
        Path = Path.Combine(imagesPath, $"{x.Id}.jpg")
    });
    var q = db.Answers.Take(5);

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