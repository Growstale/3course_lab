using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/VAV", async (HttpContext context) => {
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($"GET-Http-VAV:ParmA = {context.Request.Query["ParmA"]},ParmB = {context.Request.Query["ParmB"]}");
});


app.MapPost("/VAV", async (HttpContext context) => {
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($"POST-Http-VAV:ParmA = {context.Request.Query["ParmA"]},ParmB = {context.Request.Query["ParmB"]}");
});


app.MapPut("/VAV", async (HttpContext context) => {
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($"PUT-Http-VAV:ParmA = {context.Request.Query["ParmA"]},ParmB = {context.Request.Query["ParmB"]}");
});


/*
app.MapPost("/SUM", (HttpContext context) => {
    int x = int.Parse(context.Request.Query["X"]);
    int y = int.Parse(context.Request.Query["Y"]);
    int sum = x + y;
    context.Response.ContentType = "text/plain";
    return context.Response.WriteAsync(sum.ToString());
});
*/


app.MapPost("/SUM", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    int x = int.Parse(form["X"]);
    int y = int.Parse(form["Y"]);
    int sum = x + y;
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync(sum.ToString());
});


app.Map("/task5", async (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Умножение чисел</title>
            </head>
            <body>
                <h1>Умножение чисел</h1>
                <form id='form'>
                    <label for='x'>X:</label>
                    <input type='number' id='x' name='x' value='0'><br><br>
                    <label for='y'>Y:</label>
                    <input type='number' id='y' name='y' value='0'><br><br>
                    <button type='button' onclick='calculate()'>Умножить</button>
                </form>
                <div id='result'></div>
                <script>
                    function calculate() {
                        var x = document.getElementById('x').value;
                        var y = document.getElementById('y').value;

                        var xhr = new XMLHttpRequest();
                        xhr.open('POST', '/task5', true);
                        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                        xhr.onload = function() {
                            if (this.status == 200) {
                                document.getElementById('result').innerHTML = 'Результат: ' + this.responseText;
                            }
                        };
                        var formData = 'x=' + x + '&y=' + y;

                        xhr.send(formData);
                    }
                </script>
            </body>
            </html>
        ");
    }
    else if (context.Request.Method == "POST")
    {
        var form = await context.Request.ReadFormAsync();
        int x = int.Parse(form["x"]);
        int y = int.Parse(form["y"]);
        int product = x * y;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(product.ToString());
    }
});

app.Map("/task6", async (HttpContext context) => {
    if (context.Request.Method == "GET")
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Умножение чисел</title>
            </head>
            <body>
                <h1>Умножение чисел</h1>
                <form method='post' action='/task6'>
                    <label for='x'>X:</label>
                    <input type='number' id='x' name='x' value='0'><br><br>
                    <label for='y'>Y:</label>
                    <input type='number' id='y' name='y' value='0'><br><br>
                    <button type='submit'>Умножить</button>
                </form>
                <div id='result'></div>
            </body>
            </html>
        ");
    }
    else if (context.Request.Method == "POST")
    {
        var form = await context.Request.ReadFormAsync();
        int x = int.Parse(form["x"]);
        int y = int.Parse(form["y"]);
        int product = x * y;

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Умножение чисел</title>
            </head>
            <body>
                <h1>Результат: {product}</h1> 
            </body>
            </html>
        ");
    }
});

app.Run();
