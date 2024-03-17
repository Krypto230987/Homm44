using System;
using System.Net;

string url = "http://localhost:7777/";
HttpListener listener = new HttpListener();
listener.Prefixes.Add(url);
listener.Start();
Console.WriteLine("Listening for connections on {0}", url);

while (true)
{
    HttpListenerContext context = listener.GetContext();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    if (request.Url.LocalPath == "/")
    {
        SendResponse(response, 200,
            "<html><head><style>body { font-family: Arial; }</style></head><body><h1>Follow the white rabbit</h1></body></html>");
    }
    else if (request.Url.LocalPath == "/white_rabbit")
    {
        SendResponse(response, 200,
            "<html><head><style>body { font-family: Arial; }</style></head><body><h1 style='color: green;'>You are living in the matrix</h1></body></html>");
    }
    else
    {
        SendResponse(response, 501,
            "<html><head><style>body { font-family: Arial; }</style></head><body><h1>Not implemented</h1></body></html>");
    }
}

listener.Stop();
Console.WriteLine("Connection closed");
listener.Close();

static void SendResponse(HttpListenerResponse response, int statusCode, string responseString)
{
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    response.StatusCode = statusCode;
    response.ContentLength64 = buffer.Length;
    response.OutputStream.Write(buffer, 0, buffer.Length);
    response.Close();
}
