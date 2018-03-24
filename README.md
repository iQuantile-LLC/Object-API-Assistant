Object API Helper
================

Object API helper is designed for Object Oriented programmers to call RESTful API's using Class Objects.Let the Library handle the rest! 

Please feel free to fork and submit pull requests to the develop branch.






## Installation

It is recommended to use NuGet. F.ex through the VS Package Manager Console `Install-Package <package>` or using the VS "Manage NuGet Packages..." extension. 


## How to use

Create a model class of the API you wish to call. For example we are going to use a open API [Battuta](https://battuta.medunes.net/) that gives us Country list.

#### Class Model

```c#
public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
    }
```
#### Constructor

Now we need to create an instance of the API Helper class.You need to pass the class object template that you wish to request over API's and pass the base url through the constructor.

```c#
using System.Windows.Forms;
using ObjectAPIAssistant;

namespace Test.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ApiAssistant<Country> assistant=new ApiAssistant<Country>("http://battuta.medunes.net/api/");
             
        }
    }
}
```

But the API we wish to call returns a list of Countries. So in that case we just need to pass a list of countries as the template.

```c#
using System.Collections.Generic;
using System.Windows.Forms;
using ObjectAPIAssistant;

namespace Test.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ApiAssistant<List<Country>> assistant=new ApiAssistant<List<Country>>("http://battuta.medunes.net/api/");
             
        }
    }
}
```

# GET

Now our API helper is ready. We need to update the request URI. Get your API key from [Battuta](https://battuta.medunes.net/) if you are going to test it with that api

```c#
 assistant.RequestUri = "country/all/?key={YOUR_API_KEY}";
```
Now lets get that list of Countries from them. In one line of code.

```c#
Countries = await assistant.GetObjectAsync();
```

Incase you wish to get a HTTP Response Message object and handle the Message Manually


```c#
 HttpResponseMessage message = await assistant.GetResponseAsync();
```

# POST

Posting works the same way as get method. But make sure you Update your request URI before calling any other function

```c#
assistant.RequestUri = "REQUEST_URI_HERE";
```

Pass the list of objects and it will return the list of objects after created, Given that the API provider returns the list.
```c#
Countries = await assistant.CreateObjectAsync(Countries);
```

Incase you wish to get a HTTP Response Message object and handle the Message Manually


```c#
HttpResponseMessage message = await assistant.CreateObjectResponseAsync(Countries);
```

# PUT

PUT works exactly the same way as POST.

Pass the list of objects and it will return the list of objects after updated, Given that the API provider returns the list.
```c#
assistant.RequestUri = "REQUEST_URI_HERE";
Countries = await assistant.UpdateObjectAsync(Countries);
```

Incase you wish to get a HTTP Response Message object and handle the Message Manually
```c#
HttpResponseMessage message = await assistant.UpdateObjectResponseAsync(Countries);
```

# DELETE

DELETE works a bit differently. It Takes only the id as input and returns a Status Code.

```c#
assistant.RequestUri = "REQUEST_URI_HERE";
HttpStatusCode code=await assistant.DeleteObjecAsync("ID");
```
