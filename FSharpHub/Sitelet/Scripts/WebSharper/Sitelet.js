(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Formlet,Controls,Enhance,Data,Formlet1,Concurrency,Remoting,alert,Html,Operators,Default,List,Website,BooksAdmin,Client,FPish,Seq,HTML5,JsUtils,FSSnip,Client1,GoogleGroup,jQuery,Arrays,Login,Client2,window,EventsPervasives,Msdn,News,Utils,VideosAdmin,Client3;
 Runtime.Define(Global,{
  Website:{
   BooksAdmin:{
    Client:{
     addFormlet:Runtime.Field(function()
     {
      var x,_urlInput_40_1,x1,_titleInput_43_1,formlet,x2,_authorInput_46_1,x3,_publisherInput_51_1,x4,_isbnInput_54_1,x5,_pagesCountInput_57_1,x6,_releaseDateInput_60_1,x7,_coverInput_63_1,formlet1,x8,formlet2;
      x=Controls.Input("");
      _urlInput_40_1=Enhance.WithTextLabel("URL",x);
      x1=Controls.Input("");
      _titleInput_43_1=Enhance.WithTextLabel("Title",x1);
      formlet=Enhance.WithTextLabel("Author",Controls.Input(""));
      x2=Enhance.Many(formlet);
      _authorInput_46_1=Enhance.WithFormContainer(x2);
      x3=Controls.Input("");
      _publisherInput_51_1=Enhance.WithTextLabel("Publisher",x3);
      x4=Controls.Input("");
      _isbnInput_54_1=Enhance.WithTextLabel("ISBN",x4);
      x5=Controls.Input("");
      _pagesCountInput_57_1=Enhance.WithTextLabel("Pages",x5);
      x6=Controls.Input("");
      _releaseDateInput_60_1=Enhance.WithTextLabel("Release Date",x6);
      x7=Controls.Input("");
      _coverInput_63_1=Enhance.WithTextLabel("Cover URL",x7);
      formlet1=Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Formlet1.Return(function(url)
      {
       return function(title)
       {
        return function(authors)
        {
         return function(publisher)
         {
          return function(isbn)
          {
           return function(pages)
           {
            return function(date)
            {
             return function(cover)
             {
              return[url,title,authors,publisher,isbn,pages,date,cover];
             };
            };
           };
          };
         };
        };
       };
      }),_urlInput_40_1),_titleInput_43_1),_authorInput_46_1),_publisherInput_51_1),_isbnInput_54_1),_pagesCountInput_57_1),_releaseDateInput_60_1),_coverInput_63_1);
      x8=Enhance.WithSubmitAndResetButtons(formlet1);
      formlet2=Enhance.WithFormContainer(x8);
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var url,title,authors,publisher,isbn,pages,date,cover;
       url=tupledArg[0];
       title=tupledArg[1];
       authors=tupledArg[2];
       publisher=tupledArg[3];
       isbn=tupledArg[4];
       pages=tupledArg[5];
       date=tupledArg[6];
       cover=tupledArg[7];
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(Remoting.Async("Sitelet:4",[url,title,authors,publisher,isbn,pages,date,cover]),function(arg101)
        {
         if(arg101)
          {
           alert("New book inserted successfully.");
           return Concurrency.Return(null);
          }
         else
          {
           alert("The query failed.");
           return Concurrency.Return(null);
          }
        });
       }));
      }),formlet2);
     }),
     main:function()
     {
      return Operators.add(Default.Div(List.ofArray([Default.Attr().Class("row")])),List.ofArray([Default.Div(List.ofArray([Default.H1(List.ofArray([Default.Text("Add new book")])),Default.Div(List.ofArray([Client.addFormlet()]))]))]));
     },
     tr:function(title,publisher)
     {
      return Default.TR(List.ofArray([Default.TD(List.ofArray([Default.Text(title)])),Default.TD(List.ofArray([Default.Text(publisher)]))]));
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client.main();
     }
    })
   },
   FPish:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return FPish.main();
     }
    }),
    itemLi:function(item)
    {
     var arg10;
     arg10=item.link;
     return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg10),Default.Attr().NewAttr("target","_blank"),Default.Text(item.title)]))])),Default.Div(Seq.toList(Seq.delay(function()
     {
      return Seq.map(function(y)
      {
       return Operators.add(Default.Span(List.ofArray([Default.Attr().Class("label label-info")])),List.ofArray([Default.Text(y)]));
      },item.categories);
     })))]));
    },
    main:function()
    {
     var x;
     x=Default.Div(List.ofArray([HTML5.Attr().NewAttr("data-"+"status","loading")]));
     Operators.OnAfterRender(function(elt)
     {
      return JsUtils.displayFeed("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Ftopics%2Fall",function(item)
      {
       return FPish.itemLi(item);
      },elt);
     },x);
     return x;
    }
   },
   FSSnip:{
    Client:{
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([HTML5.Attr().NewAttr("data-"+"status","loading")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:1",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         var ul,a,b;
         ul=Default.UL(List.ofArray([Default.Attr().Class("list-group")]));
         a=Concurrency.For(arg101,function(arg102)
         {
          var arg103;
          arg103=arg102.link;
          ul.AppendI(Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg103),Default.Attr().NewAttr("target","_blank"),Default.Text(arg102.title)]))])),Default.P(List.ofArray([Default.Text(arg102.description)])),Default.P(List.ofArray([Default.Text("Published "+arg102.published+" by "+arg102.author)]))])));
          return Concurrency.Return(null);
         });
         b=Concurrency.Delay(function()
         {
          elt.AppendI(ul);
          elt["HtmlProvider@32"].RemoveAttribute(elt.Body,"data-status");
          JsUtils.hideProress();
          return Concurrency.Return(null);
         });
         return Concurrency.Bind(a,function()
         {
          return b;
         });
        });
       }));
      },x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client1.main();
     }
    })
   },
   GoogleGroup:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return GoogleGroup.main();
     }
    }),
    itemLi:function(item)
    {
     var arg10;
     arg10=item.link;
     return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg10),Default.Attr().NewAttr("target","_blank"),Default.Text(item.title)]))])),Default.P(List.ofArray([Default.Text(item.contentSnippet)])),Default.P(List.ofArray([Default.Text("Author: "+item.author)]))]));
    },
    main:function()
    {
     var x;
     x=Default.Div(List.ofArray([HTML5.Attr().NewAttr("data-"+"status","loading")]));
     Operators.OnAfterRender(function(elt)
     {
      return JsUtils.displayFeed("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&callback=?&q=https%3A%2F%2Fgroups.google.com%2Fforum%2Ffeed%2Ffsharp-opensource%2Ftopics%2Frss.xml%3Fnum%3D15",function(item)
      {
       return GoogleGroup.itemLi(item);
      },elt);
     },x);
     return x;
    }
   },
   JsUtils:{
    displayFeed:function(url,itemLi,elt)
    {
     var ul;
     ul=Default.UL(List.ofArray([Default.Attr().Class("list-group")]));
     jQuery.getJSON(url,Runtime.Tupled(function(tupledArg)
     {
      return Arrays.iter(function(x)
      {
       return ul.AppendI(itemLi(x));
      },tupledArg[0].responseData.feed.entries);
     }));
     elt.AppendI(ul);
     elt["HtmlProvider@32"].RemoveAttribute(elt.Body,"data-status");
     return JsUtils.hideProress();
    },
    hideProress:function()
    {
     return jQuery("[data-status=\"loading\"]").length===0?jQuery("#progress-bar").slideUp():jQuery("[data-spy=\"scroll\"]").each(function()
     {
      return jQuery(this).scrollspy.call(null,"refresh");
     });
    }
   },
   Login:{
    Client:{
     loginForm:function(redirectUrl)
     {
      var userInput,x,arg00,submitBtn,arg102,arg103,arg104,arg105;
      userInput=Default.Input(List.ofArray([Default.Attr().Class("form-control"),Default.Attr().NewAttr("id","username"),Default.Attr().NewAttr("type","text"),HTML5.Attr().NewAttr("autofocus","autofocus")]));
      x=Operators.add(Default.Button(List.ofArray([Default.Attr().Class("btn btn-primary btn-block"),Default.Attr().NewAttr("id","login-btn"),Default.Attr().NewAttr("type","button")])),List.ofArray([Default.Text("Submit")]));
      arg00=function()
      {
       return function()
       {
        return Concurrency.Start(Concurrency.Delay(function()
        {
         return Concurrency.Bind(Remoting.Async("Sitelet:2",[{
          Name:userInput.get_Value(),
          Password:Client2.passInput().get_Value()
         }]),function(arg101)
         {
          if(arg101.$==1)
           {
            window.location.assign(redirectUrl);
            return Concurrency.Return(null);
           }
          else
           {
            alert("Login failed");
            return Concurrency.Return(null);
           }
         });
        }));
       };
      };
      EventsPervasives.Events().OnClick(arg00,x);
      submitBtn=x;
      arg102=List.ofArray([Default.Attr().Class("form-group")]);
      arg103=List.ofArray([Default.Text("Username"),Default.Attr().NewAttr("for","username")]);
      arg104=List.ofArray([Default.Text("Password"),Default.Attr().NewAttr("for","password")]);
      arg105=List.ofArray([submitBtn]);
      return Operators.add(Default.Form(List.ofArray([Default.Attr().NewAttr("role","form"),Default.Attr().NewAttr("id","signin")])),List.ofArray([Default.H2(List.ofArray([Default.Text("Please sign in")])),Operators.add(Default.Tags().NewTag("fieldset",arg102),List.ofArray([Default.Tags().NewTag("label",arg103),userInput,Default.Tags().NewTag("label",arg104),Client2.passInput()])),Default.Tags().NewTag("fieldset",arg105)]));
     },
     passInput:Runtime.Field(function()
     {
      var x,arg00;
      x=Default.Input(List.ofArray([Default.Attr().Class("form-control"),Default.Attr().NewAttr("id","password"),Default.Attr().NewAttr("type","password")]));
      arg00=function()
      {
       return function(keyCode)
       {
        return keyCode.KeyCode===13?jQuery("#login-btn").click():null;
       };
      };
      EventsPervasives.Events().OnKeyDown(arg00,x);
      return x;
     })
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client2.loginForm(this.redirectUrl);
     }
    })
   },
   Msdn:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return Msdn.main();
     }
    }),
    itemLi:function(item)
    {
     var arg10;
     arg10=item.link;
     return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg10),Default.Attr().NewAttr("target","_blank"),Default.Text(item.title)]))])),Default.P(List.ofArray([Default.Text(item.contentSnippet)]))]));
    },
    main:function()
    {
     var x;
     x=Default.Div(List.ofArray([HTML5.Attr().NewAttr("data-"+"status","loading")]));
     Operators.OnAfterRender(function(elt)
     {
      return JsUtils.displayFeed("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=https%3A%2F%2Fsocial.msdn.microsoft.com%2FForums%2Fen-US%2Ffsharpgeneral%2Fthreads%3FoutputAs%3Drss",function(item)
      {
       return Msdn.itemLi(item);
      },elt);
     },x);
     return x;
    }
   },
   News:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return News.main();
     }
    }),
    itemLi:function(item)
    {
     var arg10;
     arg10=item.link;
     return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg10),Default.Attr().NewAttr("target","_blank"),Default.Text(item.title)]))])),Default.P(List.ofArray([Default.Text(item.contentSnippet)]))]));
    },
    main:function()
    {
     var x;
     x=Default.Div(List.ofArray([HTML5.Attr().NewAttr("data-"+"status","loading")]));
     Operators.OnAfterRender(function(elt)
     {
      return JsUtils.displayFeed("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Fblogs%2Ftag%2F1%2Ff~23",function(item)
      {
       return News.itemLi(item);
      },elt);
     },x);
     return x;
    }
   },
   Utils:{
    skip:function(xs,count)
    {
     return Seq.skip(count,xs);
    },
    split:function(count,xs)
    {
     var loop;
     loop=function(xs1)
     {
      return Seq.toList(Seq.delay(function()
      {
       return Seq.append([Utils.truncate(xs1,count)],Seq.delay(function()
       {
        return Seq.length(xs1)<=count?Seq.empty():loop(Utils.skip(xs1,count));
       }));
      }));
     };
     return loop(xs);
    },
    truncate:function(xs,count)
    {
     return Seq.truncate(count,xs);
    }
   },
   VideosAdmin:{
    Client:{
     main:function()
     {
      var x,titleInput,x1,urlInput,x2,thumbnailInput,x3,websiteInput,x4,dateInput,formlet,x5,formlet1;
      x=Controls.Input("");
      titleInput=Enhance.WithTextLabel("Title",x);
      x1=Controls.Input("");
      urlInput=Enhance.WithTextLabel("URL",x1);
      x2=Controls.Input("");
      thumbnailInput=Enhance.WithTextLabel("Thumbnail",x2);
      x3=Controls.Input("");
      websiteInput=Enhance.WithTextLabel("Website",x3);
      x4=Controls.Input("");
      dateInput=Enhance.WithTextLabel("Date",x4);
      formlet=Data.$(Data.$(Data.$(Data.$(Data.$(Formlet1.Return(function(title)
      {
       return function(url)
       {
        return function(thumb)
        {
         return function(website)
         {
          return function(date)
          {
           return[title,url,thumb,website,date];
          };
         };
        };
       };
      }),titleInput),urlInput),thumbnailInput),websiteInput),dateInput);
      x5=Enhance.WithSubmitAndResetButtons(formlet);
      formlet1=Enhance.WithFormContainer(x5);
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var title,url,thumb,website,date;
       title=tupledArg[0];
       url=tupledArg[1];
       thumb=tupledArg[2];
       website=tupledArg[3];
       date=tupledArg[4];
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(Remoting.Async("Sitelet:0",[title,url,thumb,website,date]),function(arg101)
        {
         if(arg101)
          {
           alert("New video inserted successfully.");
           return Concurrency.Return(null);
          }
         else
          {
           alert("The query failed.");
           return Concurrency.Return(null);
          }
        });
       }));
      }),formlet1);
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client3.main();
     }
    })
   }
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Formlet=Runtime.Safe(WebSharper.Formlet);
  Controls=Runtime.Safe(Formlet.Controls);
  Enhance=Runtime.Safe(Formlet.Enhance);
  Data=Runtime.Safe(Formlet.Data);
  Formlet1=Runtime.Safe(Formlet.Formlet);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  alert=Runtime.Safe(Global.alert);
  Html=Runtime.Safe(WebSharper.Html);
  Operators=Runtime.Safe(Html.Operators);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  Website=Runtime.Safe(Global.Website);
  BooksAdmin=Runtime.Safe(Website.BooksAdmin);
  Client=Runtime.Safe(BooksAdmin.Client);
  FPish=Runtime.Safe(Website.FPish);
  Seq=Runtime.Safe(WebSharper.Seq);
  HTML5=Runtime.Safe(Default.HTML5);
  JsUtils=Runtime.Safe(Website.JsUtils);
  FSSnip=Runtime.Safe(Website.FSSnip);
  Client1=Runtime.Safe(FSSnip.Client);
  GoogleGroup=Runtime.Safe(Website.GoogleGroup);
  jQuery=Runtime.Safe(Global.jQuery);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Login=Runtime.Safe(Website.Login);
  Client2=Runtime.Safe(Login.Client);
  window=Runtime.Safe(Global.window);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  Msdn=Runtime.Safe(Website.Msdn);
  News=Runtime.Safe(Website.News);
  Utils=Runtime.Safe(Website.Utils);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client3=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  Client2.passInput();
  Client.addFormlet();
  return;
 });
}());
