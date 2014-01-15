(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Formlet,Controls,Enhance,Data,Formlet1,Concurrency,Remoting,alert,Html,Operators,Default,List,Website,BooksAdmin,Client,HTML5,Login,Client1,window,EventsPervasives,jQuery,Questions,Client2,T,Snippets,Client3,Tweets,Client4,Seq,VideosAdmin,Client5;
 Runtime.Define(Global,{
  Website:{
   BooksAdmin:{
    Client:{
     addFormlet:Runtime.Field(function()
     {
      var _urlInput_40_1,x,f,_titleInput_43_1,x1,f1,_authorInput_46_1,formlet1,formlet2,x2,f2,_publisherInput_51_1,x3,f3,_isbnInput_54_1,x4,f4,_pagesCountInput_57_1,x5,f5,_releaseDateInput_60_1,x6,f6,_coverInput_63_1,x7,f7,formlet3,formlet4,formlet5;
      _urlInput_40_1=(x=Controls.Input(""),(f=function(formlet)
      {
       return Enhance.WithTextLabel("URL",formlet);
      },f(x)));
      _titleInput_43_1=(x1=Controls.Input(""),(f1=function(formlet)
      {
       return Enhance.WithTextLabel("Title",formlet);
      },f1(x1)));
      _authorInput_46_1=(formlet1=(formlet2=(x2=Controls.Input(""),(f2=function(formlet)
      {
       return Enhance.WithTextLabel("Author",formlet);
      },f2(x2))),Enhance.Many(formlet2)),Enhance.WithFormContainer(formlet1));
      _publisherInput_51_1=(x3=Controls.Input(""),(f3=function(formlet)
      {
       return Enhance.WithTextLabel("Publisher",formlet);
      },f3(x3)));
      _isbnInput_54_1=(x4=Controls.Input(""),(f4=function(formlet)
      {
       return Enhance.WithTextLabel("ISBN",formlet);
      },f4(x4)));
      _pagesCountInput_57_1=(x5=Controls.Input(""),(f5=function(formlet)
      {
       return Enhance.WithTextLabel("Pages",formlet);
      },f5(x5)));
      _releaseDateInput_60_1=(x6=Controls.Input(""),(f6=function(formlet)
      {
       return Enhance.WithTextLabel("Release Date",formlet);
      },f6(x6)));
      _coverInput_63_1=(x7=Controls.Input(""),(f7=function(formlet)
      {
       return Enhance.WithTextLabel("Cover URL",formlet);
      },f7(x7)));
      formlet3=(formlet4=(formlet5=Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Formlet1.Return(function(url)
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
      }),_urlInput_40_1),_titleInput_43_1),_authorInput_46_1),_publisherInput_51_1),_isbnInput_54_1),_pagesCountInput_57_1),_releaseDateInput_60_1),_coverInput_63_1),Enhance.WithSubmitAndResetButtons(formlet5)),Enhance.WithFormContainer(formlet4));
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var url,title,authors,publisher,isbn,pages,date,cover,arg00,clo1,t;
       url=tupledArg[0];
       title=tupledArg[1];
       authors=tupledArg[2];
       publisher=tupledArg[3];
       isbn=tupledArg[4];
       pages=tupledArg[5];
       date=tupledArg[6];
       cover=tupledArg[7];
       arg00=Concurrency.Delay((clo1=function()
       {
        var x8,f8;
        x8=Remoting.Async("Sitelet:6",[url,title,authors,publisher,isbn,pages,date,cover]);
        f8=function(_arg1)
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
        };
        return Concurrency.Bind(x8,f8);
       },clo1));
       t={
        $:0
       };
       return Concurrency.Start(arg00);
      }),formlet3);
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
      var userInput,_this,_this1,_this2,submitBtn,x,el,_this3,_this4,inner,f,arg00,_this5,x3,_this6,x4,_this7,_this8,x5,_this9,_thisa,x6,_thisb;
      userInput=Default.Input(List.ofArray([Default.Attr().Class("form-control"),(_this=Default.Attr(),_this.NewAttr("id","username")),(_this1=Default.Attr(),_this1.NewAttr("type","text")),(_this2=HTML5.Attr(),_this2.NewAttr("autofocus","autofocus"))]));
      submitBtn=(x=(el=Default.Button(List.ofArray([Default.Attr().Class("btn btn-primary btn-block"),(_this3=Default.Attr(),_this3.NewAttr("id","login-btn")),(_this4=Default.Attr(),_this4.NewAttr("type","button"))])),(inner=Default.Text("Submit"),Operators.add(el,List.ofArray([inner])))),(f=(arg00=function()
      {
       return function()
       {
        var x1,f1,f3;
        x1=(f1=function()
        {
         var info,x2,f2;
         info={
          Name:userInput.get_Value(),
          Password:Client1.passInput().get_Value()
         };
         x2=Remoting.Async("Sitelet:4",[info]);
         f2=function(_arg11)
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
         };
         return Concurrency.Bind(x2,f2);
        },Concurrency.Delay(f1));
        f3=function(arg001)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg001);
        };
        return f3(x1);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(arg00,arg10);
      }),(f(x),x)));
      return Operators.add(Default.Form(List.ofArray([Default.Attr().NewAttr("role","form"),(_this5=Default.Attr(),_this5.NewAttr("id","signin"))])),List.ofArray([Default.H2(List.ofArray([Default.Text("Please sign in")])),Operators.add((x3=List.ofArray([Default.Attr().Class("form-group")]),(_this6=Default.Tags(),_this6.NewTag("fieldset",x3))),List.ofArray([(x4=List.ofArray([Default.Text("Username"),(_this7=Default.Attr(),_this7.NewAttr("for","username"))]),(_this8=Default.Tags(),_this8.NewTag("label",x4))),userInput,(x5=List.ofArray([Default.Text("Password"),(_this9=Default.Attr(),_this9.NewAttr("for","password"))]),(_thisa=Default.Tags(),_thisa.NewTag("label",x5))),Client1.passInput()])),(x6=List.ofArray([submitBtn]),(_thisb=Default.Tags(),_thisb.NewTag("fieldset",x6)))]));
     },
     passInput:Runtime.Field(function()
     {
      var x,_this,_this1,f,arg00;
      x=Default.Input(List.ofArray([Default.Attr().Class("form-control"),(_this=Default.Attr(),_this.NewAttr("id","password")),(_this1=Default.Attr(),_this1.NewAttr("type","password"))]));
      f=(arg00=function()
      {
       return function(keyCode)
       {
        var matchValue;
        matchValue=keyCode.KeyCode;
        if(matchValue===13)
         {
          return jQuery("#login-btn").click();
         }
        else
         {
          return null;
         }
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnKeyDown(arg00,arg10);
      });
      f(x);
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
      var ul,x,f,f2;
      ul=Default.UL(List.ofArray([Default.Attr().Class("list-group"),Default.Id("questions-list")]));
      x=(f=function(array)
      {
       var f1;
       f1=Runtime.Tupled(function(tupledArg)
       {
        var link,title,date,website,summary;
        link=tupledArg[0];
        title=tupledArg[1];
        date=tupledArg[2];
        website=tupledArg[3];
        summary=tupledArg[4];
        return Client2.li(link,title,date,website,summary);
       });
       return array.map(function(x1)
       {
        return f1(x1);
       });
      },f(arr));
      f2=function(array)
      {
       var f1;
       f1=function(arg00)
       {
        return ul.AppendI(arg00);
       };
       return array.forEach(function(x1)
       {
        f1(x1);
       });
      };
      f2(x);
      return elt.AppendI(ul);
     },
     li:function(link,title,date,website,summary)
     {
      var _this,x,_this1,x1,_this2;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([Default.Text(date+", "+website)]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     main:function()
     {
      var x,f;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      f=function(w)
      {
       return Operators.OnAfterRender(function(elt)
       {
        var x1,f1,f3;
        x1=(f1=function()
        {
         var x2,f2;
         x2=Remoting.Async("Sitelet:3",[]);
         f2=function(_arg1)
         {
          var questions;
          if(_arg1.$==1)
           {
            questions=_arg1.$0;
            Client2.displayQuestions(questions,elt);
            return Concurrency.Return(null);
           }
          else
           {
            return Concurrency.Return(null);
           }
         };
         return Concurrency.Bind(x2,f2);
        },Concurrency.Delay(f1));
        f3=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f3(x1);
       },w);
      };
      f(x);
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
      var ul,x,f,f2;
      ul=Default.UL(List.ofArray([Default.Attr().Class("list-group")]));
      x=(f=function(array)
      {
       var f1;
       f1=Runtime.Tupled(function(tupledArg)
       {
        var link,title,description;
        link=tupledArg[0];
        title=tupledArg[1];
        description=tupledArg[2];
        return Client3.li(link,title,description);
       });
       return array.map(function(x1)
       {
        return f1(x1);
       });
      },f(arr));
      f2=function(array)
      {
       var f1;
       f1=function(arg00)
       {
        return ul.AppendI(arg00);
       };
       return array.forEach(function(x1)
       {
        f1(x1);
       });
      };
      f2(x);
      return elt.AppendI(ul);
     },
     li:function(link,title,description)
     {
      var _this,x,_this1;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),Default.P(List.ofArray([Default.Text(description)]))]));
     },
     main:function()
     {
      var x,f;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      f=function(w)
      {
       return Operators.OnAfterRender(function(elt)
       {
        var x1,f1,f3;
        x1=(f1=function()
        {
         var x2,f2;
         x2=Remoting.Async("Sitelet:2",[]);
         f2=function(_arg1)
         {
          var snippets;
          if(_arg1.$==1)
           {
            snippets=_arg1.$0;
            Client3.dispalySnippets(snippets,elt);
            return Concurrency.Return(null);
           }
          else
           {
            return Concurrency.Return(null);
           }
         };
         return Concurrency.Bind(x2,f2);
        },Concurrency.Delay(f1));
        f3=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f3(x1);
       },w);
      };
      f(x);
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
      var jquery;
      jquery=jQuery("a.tweet-action");
      return jquery.click(function(event)
      {
       var href,value;
       event.preventDefault();
       href=this.getAttribute("href");
       value=window.showModalDialog(href);
       value;
      });
     },
     li:function(tweet)
     {
      var id,name,screenName,profileLink,replyLink,retweetLink,favoriteLink,p,_this,x,_this1,x1,_this2,_this3,_this4;
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
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("list-group-item")])),List.ofArray([Default.Div(List.ofArray([Operators.add(Operators.add(Default.A(List.ofArray([Default.HRef(profileLink),Default.Attr().Class("profile-link"),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([Default.Img(List.ofArray([Default.Src(tweet.Avatar),Default.Alt(name),Default.Attr().Class("avatar")])),(x=List.ofArray([Default.Text(name)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),List.ofArray([Default.Text(" @"+screenName)])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([Default.Text(tweet.Date)]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),p,Operators.add(Default.Div(List.ofArray([Default.Attr().Class("tweet-actions")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(replyLink),Default.Attr().Class("tweet-action"),(_this3=Default.Attr(),_this3.NewAttr("style","margin-right: 5px;"))])),List.ofArray([Default.Text("Reply")])),Operators.add(Default.A(List.ofArray([Default.HRef(retweetLink),Default.Attr().Class("tweet-action"),(_this4=Default.Attr(),_this4.NewAttr("style","margin-right: 5px;"))])),List.ofArray([Default.Text("Retweet")])),Operators.add(Default.A(List.ofArray([Default.HRef(favoriteLink),Default.Attr().Class("tweet-action")])),List.ofArray([Default.Text("Favorite")]))]))]))]));
     },
     main:function()
     {
      var x,f;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      f=function(w)
      {
       return Operators.OnAfterRender(function(elt)
       {
        var x1,f1,f4;
        x1=(f1=function()
        {
         var x2,f2;
         x2=Remoting.Async("Sitelet:1",[]);
         f2=function(_arg1)
         {
          var tweets,ul,_this,f3,action;
          if(_arg1.$==1)
           {
            tweets=_arg1.$0;
            ul=Default.UL(List.ofArray([Default.Attr().Class("list-group"),(_this=Default.Attr(),_this.NewAttr("id","tweets-ul"))]));
            f3=(action=function(tweet)
            {
             return ul.AppendI(Client4.li(tweet));
            },function(list)
            {
             return Seq.iter(action,list);
            });
            f3(tweets);
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
         };
         return Concurrency.Bind(x2,f2);
        },Concurrency.Delay(f1));
        f4=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f4(x1);
       },w);
      };
      f(x);
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
      var titleInput,x,f,urlInput,x1,f1,thumbnailInput,x2,f2,websiteInput,x3,f3,dateInput,x4,f4,formlet1,formlet2,formlet3;
      titleInput=(x=Controls.Input(""),(f=function(formlet)
      {
       return Enhance.WithTextLabel("Title",formlet);
      },f(x)));
      urlInput=(x1=Controls.Input(""),(f1=function(formlet)
      {
       return Enhance.WithTextLabel("URL",formlet);
      },f1(x1)));
      thumbnailInput=(x2=Controls.Input(""),(f2=function(formlet)
      {
       return Enhance.WithTextLabel("Thumbnail",formlet);
      },f2(x2)));
      websiteInput=(x3=Controls.Input(""),(f3=function(formlet)
      {
       return Enhance.WithTextLabel("Website",formlet);
      },f3(x3)));
      dateInput=(x4=Controls.Input(""),(f4=function(formlet)
      {
       return Enhance.WithTextLabel("Date",formlet);
      },f4(x4)));
      formlet1=(formlet2=(formlet3=Data.$(Data.$(Data.$(Data.$(Data.$(Formlet1.Return(function(title)
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
      }),titleInput),urlInput),thumbnailInput),websiteInput),dateInput),Enhance.WithSubmitAndResetButtons(formlet3)),Enhance.WithFormContainer(formlet2));
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var title,url,thumb,website,date,arg00,clo1,t;
       title=tupledArg[0];
       url=tupledArg[1];
       thumb=tupledArg[2];
       website=tupledArg[3];
       date=tupledArg[4];
       arg00=Concurrency.Delay((clo1=function()
       {
        var x5,f5;
        x5=Remoting.Async("Sitelet:0",[title,url,thumb,website,date]);
        f5=function(_arg1)
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
        };
        return Concurrency.Bind(x5,f5);
       },clo1));
       t={
        $:0
       };
       return Concurrency.Start(arg00);
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
  Questions=Runtime.Safe(Website.Questions);
  Client2=Runtime.Safe(Questions.Client);
  T=Runtime.Safe(List.T);
  Snippets=Runtime.Safe(Website.Snippets);
  Client3=Runtime.Safe(Snippets.Client);
  Tweets=Runtime.Safe(Website.Tweets);
  Client4=Runtime.Safe(Tweets.Client);
  Seq=Runtime.Safe(WebSharper.Seq);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client5=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  Client1.passInput();
  Client.addFormlet();
 });
}());
