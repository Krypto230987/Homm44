using System.Net;
using System.Text;
string url = "http://localhost:7777/mysite/";
HttpListener listener = new HttpListener();
listener.Prefixes.Add(url);
listener.Start();
Console.WriteLine("Listening for connections on {0}", url);

while (true)
{
    HttpListenerContext context = listener.GetContext();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;
    string requestUrl = request.Url.LocalPath.TrimEnd('/');

    if (!requestUrl.Contains('.'))
    {
        string customResponse = $"<h1>{requestUrl}</h1>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(customResponse);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.Close();
    }
}

listener.Stop();
Console.WriteLine("Connection closed");
listener.Close();