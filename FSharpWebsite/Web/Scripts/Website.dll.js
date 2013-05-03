(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Website,AddThis,WebSharper,Html,Default,HTML5,List,T,BooksAdmin,Client,Formlet,Controls,Enhance,Data,Formlet1,Remoting,alert,Concurrency,Operators,Arrays,Feedback,Client1,EventsPervasives,jQuery,ForkMe,Login,Client2,window,NewsAdmin,Client3,Questions,Client4,Snippets,Client5,RegExp,Tweets,Client6,String,Videos,Client7,VideosAdmin,Client8;
 Runtime.Define(Global,{
  Website:{
   AddThis:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return AddThis.main();
     }
    }),
    main:function()
    {
     var section,_this,x;
     section=(_this=HTML5.Tags(),(x=Runtime.New(T,{
      $:0
     }),_this.NewTag("section",x)));
     section.set_Html("<div class=\"addthis_toolbox addthis_default_style \">\r\n               <a class=\"addthis_button_facebook_like\" fb:like:layout=\"button_count\"></a>\r\n               <a class=\"addthis_button_tweet\"></a>\r\n               <a class=\"addthis_button_pinterest_pinit\"></a>\r\n               <a class=\"addthis_counter addthis_pill_style\"></a>\r\n               </div>\r\n               <script type=\"text/javascript\" src=\"http://s7.addthis.com/js/300/addthis_widget.js#pubid=ra-50af450141ce9366\"></script>");
     return section;
    }
   },
   BooksAdmin:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client.main();
      }
     }),
     addFormlet:Runtime.Field(function()
     {
      var _urlInput_43_1,x,f,_titleInput_46_1,x1,f1,_authorInput_49_1,x2,x3,x4,f2,f3,f4,_publisherInput_54_1,x5,f5,_isbnInput_57_1,x6,f6,_pagesCountInput_60_1,x7,f7,_releaseDateInput_63_1,x8,f8,_coverInput_66_1,x9,f9,formlet1,xa,xb,xc,fa,fb;
      _urlInput_43_1=(x=Controls.Input(""),(f=function(formlet)
      {
       return Enhance.WithTextLabel("URL",formlet);
      },f(x)));
      _titleInput_46_1=(x1=Controls.Input(""),(f1=function(formlet)
      {
       return Enhance.WithTextLabel("Title",formlet);
      },f1(x1)));
      _authorInput_49_1=(x2=(x3=(x4=Controls.Input(""),(f2=function(formlet)
      {
       return Enhance.WithTextLabel("Author",formlet);
      },f2(x4))),(f3=function(formlet)
      {
       return Enhance.Many(formlet);
      },f3(x3))),(f4=function(formlet)
      {
       return Enhance.WithFormContainer(formlet);
      },f4(x2)));
      _publisherInput_54_1=(x5=Controls.Input(""),(f5=function(formlet)
      {
       return Enhance.WithTextLabel("Publisher",formlet);
      },f5(x5)));
      _isbnInput_57_1=(x6=Controls.Input(""),(f6=function(formlet)
      {
       return Enhance.WithTextLabel("ISBN",formlet);
      },f6(x6)));
      _pagesCountInput_60_1=(x7=Controls.Input(""),(f7=function(formlet)
      {
       return Enhance.WithTextLabel("Pages",formlet);
      },f7(x7)));
      _releaseDateInput_63_1=(x8=Controls.Input(""),(f8=function(formlet)
      {
       return Enhance.WithTextLabel("Release Date",formlet);
      },f8(x8)));
      _coverInput_66_1=(x9=Controls.Input(""),(f9=function(formlet)
      {
       return Enhance.WithTextLabel("Cover URL",formlet);
      },f9(x9)));
      formlet1=(xa=(xb=Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$(Data.$((xc=function(url)
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
      },Formlet1.Return(xc)),_urlInput_43_1),_titleInput_46_1),_authorInput_49_1),_publisherInput_54_1),_isbnInput_57_1),_pagesCountInput_60_1),_releaseDateInput_63_1),_coverInput_66_1),(fa=function(formlet)
      {
       return Enhance.WithSubmitAndResetButtons(formlet);
      },fa(xb))),(fb=function(formlet)
      {
       return Enhance.WithFormContainer(formlet);
      },fb(xa)));
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var url,title,authors,publisher,isbn,pages,date,cover,xd,fc,fe;
       url=tupledArg[0];
       title=tupledArg[1];
       authors=tupledArg[2];
       publisher=tupledArg[3];
       isbn=tupledArg[4];
       pages=tupledArg[5];
       date=tupledArg[6];
       cover=tupledArg[7];
       xd=(fc=function()
       {
        var xe,fd;
        xe=Remoting.Async("Website:3",[url,title,authors,publisher,isbn,pages,date,cover]);
        fd=function(_arg1)
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
        return Concurrency.Bind(xe,fd);
       },Concurrency.Delay(fc));
       fe=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return fe(xd);
      }),formlet1);
     }),
     booksTable:Runtime.Field(function()
     {
      var x,f,f1;
      x=Operators.add(Default.Table(List.ofArray([Default.Attr().Class("table table-striped")])),List.ofArray([Default.TR(List.ofArray([Default.TH(List.ofArray([Default.Text("Title")])),Default.TH(List.ofArray([Default.Text("Publisher")]))]))]));
      f=(f1=function(elt)
      {
       var x1,f2,f6;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:2",[]);
        f3=function(_arg1)
        {
         var x3,f4,mapping,f5,action;
         x3=(f4=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var title,publisher;
          title=tupledArg[0];
          publisher=tupledArg[1];
          return Client.tr(title,publisher);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f4(_arg1));
         f5=(action=function(arg00)
         {
          return elt.AppendI(arg00);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f5(x3);
         return Concurrency.Return(null);
        };
        return Concurrency.Bind(x2,f3);
       },Concurrency.Delay(f2));
       f6=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f6(x1);
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     }),
     main:function()
     {
      return Operators.add(Default.Div(List.ofArray([Default.Attr().Class("row")])),List.ofArray([Operators.add(Default.Div(List.ofArray([Default.Attr().Class("span6")])),List.ofArray([Default.H2(List.ofArray([Default.Text("Add new book")])),Default.Div(List.ofArray([Client.addFormlet()]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("span6")])),List.ofArray([Default.H2(List.ofArray([Default.Text("Books in database")])),Client.booksTable()]))]));
     },
     tr:function(title,publisher)
     {
      return Default.TR(List.ofArray([Default.TD(List.ofArray([Default.Text(title)])),Default.TD(List.ofArray([Default.Text(publisher)]))]));
     }
    }
   },
   Feedback:{
    Client:{
     btn:Runtime.Field(function()
     {
      var x,_this,f,x1;
      x=Operators.add(Default.Button(List.ofArray([(_this=Default.Attr(),_this.NewAttr("type","button")),Default.Attr().Class("btn btn-primary"),Default.Id("submit-btn")])),List.ofArray([Default.Text("Submit")]));
      f=(x1=function()
      {
       return function()
       {
        return Client1.sendFeedback();
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      });
      f(x);
      return x;
     }),
     form:Runtime.Field(function()
     {
      var _this;
      return Operators.add(Default.Form(List.ofArray([(_this=Default.Attr(),_this.NewAttr("style","width: 300px"))])),List.ofArray([Client1.textarea(),Default.Br(Runtime.New(T,{
       $:0
      })),Client1.btn()]));
     }),
     main:function()
     {
      var _this,x,_this1,f,x1,_this2;
      return Operators.add(Default.Div(List.ofArray([(_this=Default.Attr(),_this.NewAttr("style","position: fixed; bottom: 5px; right: 50px; background-color: white;"))])),List.ofArray([(x=Operators.add(Default.Div(List.ofArray([(_this1=Default.Attr(),_this1.NewAttr("style","font-weight: bold; background-color: black; color: white; padding: 10px; cursor: pointer;"))])),List.ofArray([Default.Text("Feedback")])),(f=(x1=function()
      {
       return function()
       {
        jQuery("#form").toggle();
        return jQuery("#message").focus();
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x))),Operators.add(Default.Div(List.ofArray([Default.Id("form"),(_this2=Default.Attr(),_this2.NewAttr("style","display: none; margin: 10px;"))])),List.ofArray([Client1.form()]))]));
     },
     sendFeedback:function()
     {
      var x,f,f4;
      x=(f=function()
      {
       var message,x1,f1;
       jQuery("#submit-btn").attr("disabled","disabled");
       message=Client1.textarea().get_Value();
       x1=Remoting.Async("Website:7",[message]);
       f1=function(_arg1)
       {
        var a,b,f2,f3;
        a=_arg1?(Client1.textarea().set_Value(""),(jQuery("#form").toggle(),(alert("Your feedback was sent."),Concurrency.Return(null)))):(alert("Failed to send your feedback. Please try again."),Concurrency.Return(null));
        b=(f2=function()
        {
         jQuery("#submit-btn").removeAttr("disabled");
         return Concurrency.Return(null);
        },Concurrency.Delay(f2));
        f3=function()
        {
         return b;
        };
        return Concurrency.Bind(a,f3);
       };
       return Concurrency.Bind(x1,f1);
      },Concurrency.Delay(f));
      f4=function(arg00)
      {
       var t;
       t={
        $:0
       };
       return Concurrency.Start(arg00);
      };
      return f4(x);
     },
     textarea:Runtime.Field(function()
     {
      var _this,_this1;
      return Default.TextArea(List.ofArray([(_this=Default.Attr(),_this.NewAttr("rows","5")),(_this1=Default.Attr(),_this1.NewAttr("style","width: 280px")),Default.Id("message")]));
     })
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client1.main();
     }
    })
   },
   ForkMe:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return ForkMe.main();
     }
    }),
    main:function()
    {
     var _this;
     return Operators.add(Default.A(List.ofArray([Default.HRef("https://github.com/TahaHachana/FSharp-Hub"),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([Default.Img(List.ofArray([Default.Src("https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"),Default.Alt("Fork me on GitHub"),Default.Id("forkme")]))]));
    }
   },
   Login:{
    Client:{
     loginForm:function(redirectUrl)
     {
      var userInput,_this,_this1,_this2,submitBtn,x,_this3,f,x1,x4,x5,_this4,x6,_this5,x7,_this6,_this7,x8,_this8;
      userInput=Default.Input(List.ofArray([(_this=Default.Attr(),_this.NewAttr("type","text")),(_this1=HTML5.Attr(),_this1.NewAttr("autofocus","autofocus")),(_this2=HTML5.Attr(),_this2.NewAttr("placeholder","username"))]));
      submitBtn=(x=Operators.add(Default.Button(List.ofArray([(_this3=Default.Attr(),_this3.NewAttr("type","button")),Default.Attr().Class("btn"),Default.Id("login-btn")])),List.ofArray([Default.Text("Submit")])),(f=(x1=function()
      {
       return function()
       {
        var x2,f1,f3;
        x2=(f1=function()
        {
         var x3,f2;
         x3=Remoting.Async("Website:6",[{
          Name:userInput.get_Value(),
          Password:Client2.passInput().get_Value()
         }]);
         f2=function(_arg11)
         {
          if(_arg11.$==1)
           {
            window.location.href=redirectUrl;
            return Concurrency.Return(null);
           }
          else
           {
            alert("Login failed");
            return Concurrency.Return(null);
           }
         };
         return Concurrency.Bind(x3,f2);
        },Concurrency.Delay(f1));
        f3=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f3(x2);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x)));
      return Default.Form(List.ofArray([(x4=List.ofArray([(x5=List.ofArray([Default.Text("Login")]),(_this4=Default.Tags(),_this4.NewTag("legend",x5))),(x6=List.ofArray([Default.Text("Username")]),(_this5=Default.Tags(),_this5.NewTag("label",x6))),userInput,(x7=List.ofArray([Default.Text("Password")]),(_this6=Default.Tags(),_this6.NewTag("label",x7))),Client2.passInput()]),(_this7=Default.Tags(),_this7.NewTag("fieldset",x4))),(x8=List.ofArray([submitBtn]),(_this8=Default.Tags(),_this8.NewTag("fieldset",x8)))]));
     },
     passInput:Runtime.Field(function()
     {
      var x,_this,_this1,f,x1;
      x=Default.Input(List.ofArray([(_this=Default.Attr(),_this.NewAttr("type","text")),(_this1=HTML5.Attr(),_this1.NewAttr("placeholder","password"))]));
      f=(x1=function()
      {
       return function(key)
       {
        var matchValue;
        matchValue=key.KeyCode;
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
       return EventsPervasives.Events().OnKeyDown(x1,arg10);
      });
      f(x);
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
   NewsAdmin:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client3.main();
      }
     }),
     main:function()
     {
      var titleInput,x,f,summaryInput,x1,f1,urlInput,x2,f2,dateInput,x3,f3,formlet1,x4,x5,x6,f4,f5;
      titleInput=(x=Controls.Input(""),(f=function(formlet)
      {
       return Enhance.WithTextLabel("Title",formlet);
      },f(x)));
      summaryInput=(x1=Controls.TextArea(""),(f1=function(formlet)
      {
       return Enhance.WithTextLabel("Summary",formlet);
      },f1(x1)));
      urlInput=(x2=Controls.Input(""),(f2=function(formlet)
      {
       return Enhance.WithTextLabel("URL",formlet);
      },f2(x2)));
      dateInput=(x3=Controls.Input(""),(f3=function(formlet)
      {
       return Enhance.WithTextLabel("Date",formlet);
      },f3(x3)));
      formlet1=(x4=(x5=Data.$(Data.$(Data.$(Data.$((x6=function(title)
      {
       return function(summary)
       {
        return function(url)
        {
         return function(date)
         {
          return[title,summary,url,date];
         };
        };
       };
      },Formlet1.Return(x6)),titleInput),summaryInput),urlInput),dateInput),(f4=function(formlet)
      {
       return Enhance.WithSubmitAndResetButtons(formlet);
      },f4(x5))),(f5=function(formlet)
      {
       return Enhance.WithFormContainer(formlet);
      },f5(x4)));
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var title,summary,url,date,x7,f6,f8;
       title=tupledArg[0];
       summary=tupledArg[1];
       url=tupledArg[2];
       date=tupledArg[3];
       x7=(f6=function()
       {
        var x8,f7;
        x8=Remoting.Async("Website:0",[title,summary,url,date]);
        f7=function(_arg1)
        {
         if(_arg1)
          {
           alert("News item inserted successfully.");
           return Concurrency.Return(null);
          }
         else
          {
           alert("The query failed.");
           return Concurrency.Return(null);
          }
        };
        return Concurrency.Bind(x8,f7);
       },Concurrency.Delay(f6));
       f8=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f8(x7);
      }),formlet1);
     }
    }
   },
   Questions:{
    Client:{
     displayQuestions:function(arr,elt)
     {
      var ul,x,f,f1,action;
      ul=Default.UL(List.ofArray([Default.Id("questions-list")]));
      x=(f=function(array)
      {
       return Arrays.map(Runtime.Tupled(function(tupledArg)
       {
        var link,title,date,website,summary;
        link=tupledArg[0];
        title=tupledArg[1];
        date=tupledArg[2];
        website=tupledArg[3];
        summary=tupledArg[4];
        return Client4.li(link,title,date,website,summary);
       }),array);
      },f(arr));
      f1=(action=function(arg00)
      {
       return ul.AppendI(arg00);
      },function(array)
      {
       return Arrays.iter(action,array);
      });
      f1(x);
      return elt.AppendI(ul);
     },
     li:function(link,title,date,website,summary)
     {
      var x,_this,x1,x2,_this1;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("question")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link)])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this=Default.Tags(),_this.NewTag("strong",x)))])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([(x2=date+", "+website,Default.Text(x2))]),(_this1=Default.Tags(),_this1.NewTag("small",x1))),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     main:function()
     {
      var x,f,f1;
      x=Default.Div(List.ofArray([Default.Id("fsharp-questions")]));
      f=(f1=function(elt)
      {
       var x1,f2,f4;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:4",[]);
        f3=function(_arg1)
        {
         var arr;
         if(_arg1.$==1)
          {
           arr=_arg1.$0;
           Client4.displayQuestions(arr,elt);
           return Concurrency.Return(null);
          }
         else
          {
           return Concurrency.Return(null);
          }
        };
        return Concurrency.Bind(x2,f3);
       },Concurrency.Delay(f2));
       f4=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f4(x1);
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client4.main();
     }
    })
   },
   Snippets:{
    Client:{
     dispalySnippets:function(arr,elt)
     {
      var ul,x,f,f1,action;
      ul=Default.UL(List.ofArray([Default.Id("snippets-list")]));
      x=(f=function(array)
      {
       return Arrays.map(Runtime.Tupled(function(tupledArg)
       {
        var link,title,description;
        link=tupledArg[0];
        title=tupledArg[1];
        description=tupledArg[2];
        return Client5.li(link,title,description);
       }),array);
      },f(arr));
      f1=(action=function(arg00)
      {
       return ul.AppendI(arg00);
      },function(array)
      {
       return Arrays.iter(action,array);
      });
      f1(x);
      return elt.AppendI(ul);
     },
     li:function(link,title,description)
     {
      var x,_this;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("snippet")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link)])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this=Default.Tags(),_this.NewTag("strong",x)))])),Default.P(List.ofArray([Default.Text(description)]))]));
     },
     main:function()
     {
      var x,f,f1;
      x=Default.Div(List.ofArray([Default.Id("fsharp-snippets")]));
      f=(f1=function(elt)
      {
       var x1,f2,f4;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:5",[]);
        f3=function(_arg1)
        {
         var arr;
         if(_arg1.$==1)
          {
           arr=_arg1.$0;
           Client5.dispalySnippets(arr,elt);
           return Concurrency.Return(null);
          }
         else
          {
           return Concurrency.Return(null);
          }
        };
        return Concurrency.Bind(x2,f3);
       },Concurrency.Delay(f2));
       f4=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f4(x1);
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client5.main();
     }
    })
   },
   Tweets:{
    Client:{
     atRegex:Runtime.Field(function()
     {
      return new RegExp("(@)([A-Za-z0-9-_]+)","g");
     }),
     displayTweets:function(ul,elt)
     {
      return jQuery.getJSON("http://search.twitter.com/search.json?q=%23fsharp&amp;rpp=100&amp;callback=?",Runtime.Tupled(function(tupledArg)
      {
       var data,_arg1,x,f,action;
       data=tupledArg[0];
       _arg1=tupledArg[1];
       x=data.results;
       f=(action=function(result)
       {
        var tweetHtml,x1,f1;
        tweetHtml=(Client6.linkify())(result.text);
        x1=Client6.tweetLi(result.from_user,result.id_str,result.profile_image_url,result.from_user_name,tweetHtml,result.created_at);
        f1=function(arg00)
        {
         return ul.AppendI(arg00);
        };
        return f1(x1);
       },function(array)
       {
        return Arrays.iter(action,array);
       });
       f(x);
       return elt.AppendI(ul);
      })).then(function()
      {
       Client6.toggleActionsVisibility();
       return Client6.handleTweetActions();
      },function()
      {
       return null;
      });
     },
     handleTweetActions:function()
     {
      var jquery;
      jquery=jQuery("a.tweet-action-link");
      return jquery.click(function(event)
      {
       var href,x,f;
       event.preventDefault();
       href=this.getAttribute("href");
       x=window.showModalDialog(href);
       f=function(value)
       {
        value;
       };
       return f(x);
      });
     },
     hashRegex:Runtime.Field(function()
     {
      return new RegExp("(#)([A-Za-z0][A-Za-z0-9-_]+)","g");
     }),
     linkify:Runtime.Field(function()
     {
      var f,f1,g,g1;
      f=(f1=function(str)
      {
       return Client6.replaceUrls(str);
      },(g=function(str)
      {
       return Client6.replaceUsers(str);
      },function(x)
      {
       return g(f1(x));
      }));
      g1=function(str)
      {
       return Client6.replaceHashs(str);
      };
      return function(x)
      {
       return g1(f(x));
      };
     }),
     main:function()
     {
      var x,f,f1;
      x=Default.Div(List.ofArray([Default.Id("fsharp-tweets")]));
      f=(f1=function(elt)
      {
       var x1,f2,f4;
       x1=(f2=function()
       {
        var ul,x2,f3;
        ul=Default.UL(List.ofArray([Default.Id("tweets-list")]));
        x2=Client6.displayTweets(ul,elt);
        f3=function(value)
        {
         value;
        };
        f3(x2);
        return Concurrency.Return(null);
       },Concurrency.Delay(f2));
       f4=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f4(x1);
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     },
     replaceHashs:function(str)
     {
      return(new String(str)).replace(Client6.hashRegex(),"<a href=\"https://twitter.com/search/?q=%23$2\" target=\"_blank\">#$2</a>");
     },
     replaceUrls:function(str)
     {
      return(new String(str)).replace(Client6.urlRegex(),"<a href=\"$1\" target=\"_blank\">$1</a>");
     },
     replaceUsers:function(str)
     {
      return(new String(str)).replace(Client6.atRegex(),"<a href=\"https://twitter.com/$2\" target=\"_blank\">@$2</a>");
     },
     toggleActionsVisibility:function()
     {
      var jquery;
      jquery=jQuery(".tweet");
      jquery.mouseenter(function()
      {
       return jQuery(".tweetActions",this).css("visibility","visible");
      });
      return jquery.mouseleave(function()
      {
       return jQuery(".tweetActions",this).css("visibility","hidden");
      });
     },
     tweetLi:function(screenName,tweetId,profileImage,fullName,tweetHtml,creationDate)
     {
      var profileLink,replyLink,retweetLink,favoriteLink,p,_this,_this1,x,_this2,x1,_this3,_this4,_this5,_this6;
      profileLink="https://twitter.com/"+screenName;
      replyLink="https://twitter.com/intent/tweet?in_reply_to="+tweetId;
      retweetLink="https://twitter.com/intent/retweet?tweet_id="+tweetId;
      favoriteLink="https://twitter.com/intent/favorite?tweet_id="+tweetId;
      p=Default.P(Runtime.New(T,{
       $:0
      }));
      p.set_Html(tweetHtml);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweet"),(_this=Default.Attr(),_this.NewAttr("style","clear: both;"))])),List.ofArray([Default.Div(List.ofArray([Operators.add(Operators.add(Default.A(List.ofArray([Default.HRef(profileLink),Default.Attr().Class("twitterProfileLink"),(_this1=Default.Attr(),_this1.NewAttr("target","_blank"))])),List.ofArray([Default.Img(List.ofArray([Default.Src(profileImage),Default.Alt(fullName),Default.Attr().Class("avatar"),Default.Height("48"),Default.Width("48")])),(x=List.ofArray([Default.Text(fullName)]),(_this2=Default.Tags(),_this2.NewTag("strong",x)))])),List.ofArray([Default.Text(" @"+screenName)])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([Default.Text(creationDate)]),(_this3=Default.Tags(),_this3.NewTag("small",x1))),p,Operators.add(Default.Div(List.ofArray([Default.Attr().Class("tweetActions"),(_this4=Default.Attr(),_this4.NewAttr("style","visibility: hidden;"))])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(replyLink),Default.Attr().Class("tweet-action-link"),(_this5=Default.Attr(),_this5.NewAttr("style","margin-right: 5px;"))])),List.ofArray([Default.Text("Reply")])),Operators.add(Default.A(List.ofArray([Default.HRef(retweetLink),Default.Attr().Class("tweet-action-link"),(_this6=Default.Attr(),_this6.NewAttr("style","margin-right: 5px;"))])),List.ofArray([Default.Text("Retweet")])),Operators.add(Default.A(List.ofArray([Default.HRef(favoriteLink),Default.Attr().Class("tweet-action-link")])),List.ofArray([Default.Text("Favorite")]))]))]))]));
     },
     urlRegex:Runtime.Field(function()
     {
      return new RegExp("([A-Za-z]+:\\/\\/[A-Za-z0-9-_]+\\.[A-Za-z0-9-_:%&amp;;\\?\\/.=]+)","g");
     })
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client6.main();
     }
    })
   },
   Utilities:{
    Client:{
     incrementDataCount:function(selector,dataAttribute,n)
     {
      var jquery,count,x,x1,f,f1;
      jquery=jQuery(selector);
      count=(x=(x1=jquery.attr(dataAttribute),(f=function(x2)
      {
       return(x2<<0)+n;
      },f(x1))),(f1=function(value)
      {
       return Global.String(value);
      },f1(x)));
      return jquery.attr(dataAttribute,count);
     },
     prependElement:function(selector,element)
     {
      return jQuery(selector).prepend(element.Body);
     },
     setAttributeValue:function(selector,attribute,value)
     {
      return jQuery(selector).attr(attribute,value);
     }
    }
   },
   Videos:{
    Client:{
     PagerViewer:Runtime.Class({
      get_Body:function()
      {
       return Client7.pager();
      }
     }),
     pager:function()
     {
      var x,f,f1;
      x=Operators.add(Default.UL(List.ofArray([Default.Attr().Class("pager")])),List.ofArray([Operators.add(Default.LI(List.ofArray([Default.Id("previous"),Default.Attr().Class("previous")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Id("prevLink"),Default.HRef("")])),List.ofArray([Default.Text("Prev")]))])),Operators.add(Default.LI(List.ofArray([Default.Id("next"),Default.Attr().Class("next")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Id("nextLink"),Default.HRef("")])),List.ofArray([Default.Text("Next")]))]))]));
      f=(f1=function()
      {
       var jquery,previous,x1,f2,next,x2,f3,pagesCount,x3,f4;
       jquery=jQuery("#pager");
       previous=(x1=jquery.attr("data-previous"),(f2=function(value)
       {
        return value<<0;
       },f2(x1)));
       next=(x2=jquery.attr("data-next"),(f3=function(value)
       {
        return value<<0;
       },f3(x2)));
       pagesCount=(x3=jquery.attr("data-pages-count"),(f4=function(value)
       {
        return value<<0;
       },f4(x3)));
       if(previous===0)
        {
         jQuery("#previous").addClass("disabled");
        }
       else
        {
         jQuery("#prevLink").attr("href","/videos/"+Global.String(previous));
        }
       if(next===pagesCount)
        {
         return jQuery("#next").addClass("disabled");
        }
       else
        {
         return jQuery("#nextLink").attr("href","/videos/"+Global.String(next));
        }
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     }
    }
   },
   VideosAdmin:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client8.main();
      }
     }),
     main:function()
     {
      var titleInput,x,f,urlInput,x1,f1,thumbnailInput,x2,f2,websiteInput,x3,f3,dateInput,x4,f4,formlet1,x5,x6,x7,f5,f6;
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
      formlet1=(x5=(x6=Data.$(Data.$(Data.$(Data.$(Data.$((x7=function(title)
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
      },Formlet1.Return(x7)),titleInput),urlInput),thumbnailInput),websiteInput),dateInput),(f5=function(formlet)
      {
       return Enhance.WithSubmitAndResetButtons(formlet);
      },f5(x6))),(f6=function(formlet)
      {
       return Enhance.WithFormContainer(formlet);
      },f6(x5)));
      return Formlet1.Run(Runtime.Tupled(function(tupledArg)
      {
       var title,url,thumb,website,date,x8,f7,f9;
       title=tupledArg[0];
       url=tupledArg[1];
       thumb=tupledArg[2];
       website=tupledArg[3];
       date=tupledArg[4];
       x8=(f7=function()
       {
        var x9,f8;
        x9=Remoting.Async("Website:1",[title,url,thumb,website,date]);
        f8=function(_arg1)
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
        return Concurrency.Bind(x9,f8);
       },Concurrency.Delay(f7));
       f9=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f9(x8);
      }),formlet1);
     }
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  Website=Runtime.Safe(Global.Website);
  AddThis=Runtime.Safe(Website.AddThis);
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Default=Runtime.Safe(Html.Default);
  HTML5=Runtime.Safe(Default.HTML5);
  List=Runtime.Safe(WebSharper.List);
  T=Runtime.Safe(List.T);
  BooksAdmin=Runtime.Safe(Website.BooksAdmin);
  Client=Runtime.Safe(BooksAdmin.Client);
  Formlet=Runtime.Safe(WebSharper.Formlet);
  Controls=Runtime.Safe(Formlet.Controls);
  Enhance=Runtime.Safe(Formlet.Enhance);
  Data=Runtime.Safe(Formlet.Data);
  Formlet1=Runtime.Safe(Formlet.Formlet);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  alert=Runtime.Safe(Global.alert);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Operators=Runtime.Safe(Html.Operators);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Feedback=Runtime.Safe(Website.Feedback);
  Client1=Runtime.Safe(Feedback.Client);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  jQuery=Runtime.Safe(Global.jQuery);
  ForkMe=Runtime.Safe(Website.ForkMe);
  Login=Runtime.Safe(Website.Login);
  Client2=Runtime.Safe(Login.Client);
  window=Runtime.Safe(Global.window);
  NewsAdmin=Runtime.Safe(Website.NewsAdmin);
  Client3=Runtime.Safe(NewsAdmin.Client);
  Questions=Runtime.Safe(Website.Questions);
  Client4=Runtime.Safe(Questions.Client);
  Snippets=Runtime.Safe(Website.Snippets);
  Client5=Runtime.Safe(Snippets.Client);
  RegExp=Runtime.Safe(Global.RegExp);
  Tweets=Runtime.Safe(Website.Tweets);
  Client6=Runtime.Safe(Tweets.Client);
  String=Runtime.Safe(Global.String);
  Videos=Runtime.Safe(Website.Videos);
  Client7=Runtime.Safe(Videos.Client);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client8=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  Client6.urlRegex();
  Client6.linkify();
  Client6.hashRegex();
  Client6.atRegex();
  Client2.passInput();
  Client1.textarea();
  Client1.form();
  Client1.btn();
  Client.booksTable();
  Client.addFormlet();
 });
}());
