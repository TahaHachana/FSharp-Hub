(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Formlet,Controls,Enhance,Data,Formlet1,Concurrency,Remoting,alert,Html,Operators,Default,List,Website,BooksAdmin,Client,HTML5,Login,Client1,window,EventsPervasives,jQuery,Arrays,Questions,Client2,T,Snippets,Client3,Seq,Tweets,Client4,VideosAdmin,Client5;
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
        return Concurrency.Bind(Remoting.Async("Sitelet:6",[url,title,authors,publisher,isbn,pages,date,cover]),function(_arg1)
        {
         if(_arg1)
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
   Login:{
    Client:{
     loginForm:function(redirectUrl)
     {
      var userInput,x,arg00,submitBtn,arg10,arg101,arg102,arg103;
      userInput=Default.Input(List.ofArray([Default.Attr().Class("form-control"),Default.Attr().NewAttr("id","username"),Default.Attr().NewAttr("type","text"),HTML5.Attr().NewAttr("autofocus","autofocus")]));
      x=Operators.add(Default.Button(List.ofArray([Default.Attr().Class("btn btn-primary btn-block"),Default.Attr().NewAttr("id","login-btn"),Default.Attr().NewAttr("type","button")])),List.ofArray([Default.Text("Submit")]));
      arg00=function()
      {
       return function()
       {
        return Concurrency.Start(Concurrency.Delay(function()
        {
         return Concurrency.Bind(Remoting.Async("Sitelet:4",[{
          Name:userInput.get_Value(),
          Password:Client1.passInput().get_Value()
         }]),function(_arg11)
         {
          if(_arg11.$==1)
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
      arg10=List.ofArray([Default.Attr().Class("form-group")]);
      arg101=List.ofArray([Default.Text("Username"),Default.Attr().NewAttr("for","username")]);
      arg102=List.ofArray([Default.Text("Password"),Default.Attr().NewAttr("for","password")]);
      arg103=List.ofArray([submitBtn]);
      return Operators.add(Default.Form(List.ofArray([Default.Attr().NewAttr("role","form"),Default.Attr().NewAttr("id","signin")])),List.ofArray([Default.H2(List.ofArray([Default.Text("Please sign in")])),Operators.add(Default.Tags().NewTag("fieldset",arg10),List.ofArray([Default.Tags().NewTag("label",arg101),userInput,Default.Tags().NewTag("label",arg102),Client1.passInput()])),Default.Tags().NewTag("fieldset",arg103)]));
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
      return Client1.loginForm(this.redirectUrl);
     }
    })
   },
   Questions:{
    Client:{
     displayQuestions:function(arr,elt)
     {
      var ul;
      ul=Default.UL(List.ofArray([Default.Attr().Class("list-group"),Default.Id("questions-list")]));
      Arrays.iter(function(arg00)
      {
       return ul.AppendI(arg00);
      },Arrays.map(Runtime.Tupled(function(tupledArg)
      {
       return Client2.li(tupledArg[0],tupledArg[1],tupledArg[2],tupledArg[3],tupledArg[4]);
      }),arr));
      return elt.AppendI(ul);
     },
     li:function(link,title,date,website,summary)
     {
      var arg10,arg101;
      arg10=List.ofArray([Default.Text(title)]);
      arg101=List.ofArray([Default.Text(date+", "+website)]);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Tags().NewTag("strong",arg10)])),Default.Br(Runtime.New(T,{
       $:0
      })),Default.Tags().NewTag("small",arg101),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(Remoting.Async("Sitelet:3",[]),function(_arg1)
        {
         if(_arg1.$==1)
          {
           Client2.displayQuestions(_arg1.$0,elt);
           return Concurrency.Return(null);
          }
         else
          {
           return Concurrency.Return(null);
          }
        });
       }));
      },x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client2.main();
     }
    })
   },
   Snippets:{
    Client:{
     dispalySnippets:function(arr,elt)
     {
      var ul;
      ul=Default.UL(List.ofArray([Default.Attr().Class("list-group")]));
      Arrays.iter(function(arg00)
      {
       return ul.AppendI(arg00);
      },Arrays.map(Runtime.Tupled(function(tupledArg)
      {
       return Client3.li(tupledArg[0],tupledArg[1],tupledArg[2]);
      }),arr));
      return elt.AppendI(ul);
     },
     li:function(link,title,description)
     {
      var arg10;
      arg10=List.ofArray([Default.Text(title)]);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Tags().NewTag("strong",arg10)])),Default.P(List.ofArray([Default.Text(description)]))]));
     },
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(Remoting.Async("Sitelet:2",[]),function(_arg1)
        {
         if(_arg1.$==1)
          {
           Client3.dispalySnippets(_arg1.$0,elt);
           return Concurrency.Return(null);
          }
         else
          {
           return Concurrency.Return(null);
          }
        });
       }));
      },x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client3.main();
     }
    })
   },
   Tweets:{
    Client:{
     handleTweetActions:function()
     {
      return jQuery("a.tweet-action").click(function(event)
      {
       event.preventDefault();
       window.showModalDialog(this.getAttribute("href"));
       return;
      });
     },
     li:function(tweet)
     {
      var id,name,screenName,profileLink,replyLink,retweetLink,favoriteLink,p,arg10,arg101;
      id=tweet.Id;
      name=tweet.Name;
      screenName=tweet.ScreenName;
      profileLink="https://twitter.com/"+screenName;
      replyLink="https://twitter.com/intent/tweet?in_reply_to="+id;
      retweetLink="https://twitter.com/intent/retweet?tweet_id="+id;
      favoriteLink="https://twitter.com/intent/favorite?tweet_id="+id;
      p=Default.P(Runtime.New(T,{
       $:0
      }));
      p.set_Html(tweet.Html);
      arg10=List.ofArray([Default.Text(name)]);
      arg101=List.ofArray([Default.Text(tweet.Date)]);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Default.Div(List.ofArray([Operators.add(Operators.add(Default.A(List.ofArray([Default.HRef(profileLink),Default.Attr().Class("profile-link"),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Src(tweet.Avatar),Default.Alt(name),Default.Attr().Class("avatar")])),Default.Tags().NewTag("strong",arg10)])),List.ofArray([Default.Text(" @"+screenName)])),Default.Br(Runtime.New(T,{
       $:0
      })),Default.Tags().NewTag("small",arg101),p,Operators.add(Default.Div(List.ofArray([Default.Attr().Class("tweet-actions")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(replyLink),Default.Attr().Class("tweet-action"),Default.Attr().NewAttr("style","margin-right: 5px;")])),List.ofArray([Default.Text("Reply")])),Operators.add(Default.A(List.ofArray([Default.HRef(retweetLink),Default.Attr().Class("tweet-action"),Default.Attr().NewAttr("style","margin-right: 5px;")])),List.ofArray([Default.Text("Retweet")])),Operators.add(Default.A(List.ofArray([Default.HRef(favoriteLink),Default.Attr().Class("tweet-action")])),List.ofArray([Default.Text("Favorite")]))]))]))]));
     },
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:1",[]);
        return Concurrency.Bind(x1,function(_arg1)
        {
         var tweets,ul;
         if(_arg1.$==1)
          {
           tweets=_arg1.$0;
           ul=Default.UL(List.ofArray([Default.Attr().Class("list-group"),Default.Attr().NewAttr("id","tweets-ul")]));
           Seq.iter(function(tweet)
           {
            return ul.AppendI(Client4.li(tweet));
           },tweets);
           elt.AppendI(ul);
           Client4.toggleActionsVisibility();
           Client4.handleTweetActions();
           return Concurrency.Return(null);
          }
         else
          {
           alert("Failed to fetch the latest tweets.");
           return Concurrency.Return(null);
          }
        });
       }));
      },x);
      return x;
     },
     toggleActionsVisibility:function()
     {
      var jquery;
      jquery=jQuery(".list-group-item");
      jquery.mouseenter(function()
      {
       return jQuery(".tweet-actions",this).css("visibility","visible");
      });
      return jquery.mouseleave(function()
      {
       return jQuery(".tweet-actions",this).css("visibility","hidden");
      });
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client4.main();
     }
    })
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
        return Concurrency.Bind(Remoting.Async("Sitelet:0",[title,url,thumb,website,date]),function(_arg1)
        {
         if(_arg1)
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
      return Client5.main();
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
  HTML5=Runtime.Safe(Default.HTML5);
  Login=Runtime.Safe(Website.Login);
  Client1=Runtime.Safe(Login.Client);
  window=Runtime.Safe(Global.window);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  jQuery=Runtime.Safe(Global.jQuery);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Questions=Runtime.Safe(Website.Questions);
  Client2=Runtime.Safe(Questions.Client);
  T=Runtime.Safe(List.T);
  Snippets=Runtime.Safe(Website.Snippets);
  Client3=Runtime.Safe(Snippets.Client);
  Seq=Runtime.Safe(WebSharper.Seq);
  Tweets=Runtime.Safe(Website.Tweets);
  Client4=Runtime.Safe(Tweets.Client);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client5=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  Client1.passInput();
  Client.addFormlet();
  return;
 });
}());
