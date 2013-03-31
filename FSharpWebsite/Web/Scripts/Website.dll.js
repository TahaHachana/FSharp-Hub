(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Website,AddThis,WebSharper,Html,Default,HTML5,List,T,BooksAdmin,Client,Formlet,Controls,Enhance,Data,Formlet1,Remoting,alert,Concurrency,Operators,Arrays,Forkme,Login,Client1,window,NewsAdmin,Client2,Questions,Client3,jQuery,Utilities,Client4,EventsPervasives,setInterval,Snippets,Client5,Tweets,Client6,Videos,Client7,VideosAdmin,Client8;
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
   Forkme:{
    Viewer:Runtime.Class({
     get_Body:function()
     {
      return Forkme.ribbon();
     }
    }),
    ribbon:function()
    {
     return Operators.add(Default.A(List.ofArray([Default.HRef("https://github.com/TahaHachana/FSharp-Hub")])),List.ofArray([Default.Img(List.ofArray([Default.Src("https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"),Default.Alt("Fork me on GitHub"),Default.Id("forkme")]))]));
    }
   },
   Login:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client1.loginForm(this.redirectUrl);
      }
     }),
     loginForm:function(redirectUrl)
     {
      var userName,x,f,password,x1,f1,formlet1,x2,x3,x4,f2,f3;
      userName=(x=Controls.Input(""),(f=function(formlet)
      {
       return Enhance.WithTextLabel("Username",formlet);
      },f(x)));
      password=(x1=Controls.Password(""),(f1=function(formlet)
      {
       return Enhance.WithTextLabel("Password",formlet);
      },f1(x1)));
      formlet1=(x2=(x3=Data.$(Data.$((x4=function(n)
      {
       return function(pw)
       {
        return{
         Name:n,
         Password:pw
        };
       };
      },Formlet1.Return(x4)),userName),password),(f2=function(formlet)
      {
       return Enhance.WithSubmitButton(formlet);
      },f2(x3))),(f3=function(formlet)
      {
       return Enhance.WithFormContainer(formlet);
      },f3(x2)));
      return Formlet1.Run(function(loginInfo)
      {
       var x5,f4,f6;
       x5=(f4=function()
       {
        var x6,f5;
        x6=Remoting.Async("Website:12",[loginInfo]);
        f5=function(_arg1)
        {
         if(_arg1)
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
        return Concurrency.Bind(x6,f5);
       },Concurrency.Delay(f4));
       f6=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f6(x5);
      },formlet1);
     }
    }
   },
   NewsAdmin:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client2.main();
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
     QuestionsViewer:Runtime.Class({
      get_Body:function()
      {
       return Client3.questionsDiv();
      }
     }),
     checkNewQuestions:function()
     {
      var x,f,f5;
      x=(f=function()
      {
       var jquery,latestQuestionId,x1,f1;
       jquery=jQuery("#fsharpQuestions");
       latestQuestionId=jquery.attr("data-question-id");
       x1=Remoting.Async("Website:6",[latestQuestionId]);
       f1=function(_arg1)
       {
        var questions,id,x2,x3,f2,f3,mapping,f4,action,count,msg;
        if(_arg1.$==1)
         {
          questions=_arg1.$0[1];
          id=_arg1.$0[0];
          x2=(x3=(f2=function(array)
          {
           return array.slice(0,array.length).reverse();
          },f2(questions)),(f3=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var link,title,date,website,summary;
           link=tupledArg[0];
           title=tupledArg[1];
           date=tupledArg[2];
           website=tupledArg[3];
           summary=tupledArg[4];
           return Client3.makeQuestionLi(link,title,date,website,summary);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f3(x3)));
          f4=(action=function(element)
          {
           return Client4.prependElement("#questionsList",element);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f4(x2);
          count=questions.length;
          Client3.incrementQuestionsCount(count);
          Client3.setQuestionId(id);
          msg=count===1?"1 new question":Global.String(count)+" new questions";
          Client4.displayInfoAlert(msg);
          return Concurrency.Return(null);
         }
        else
         {
          return Concurrency.Return(null);
         }
       };
       return Concurrency.Bind(x1,f1);
      },Concurrency.Delay(f));
      f5=function(arg00)
      {
       var t;
       t={
        $:0
       };
       return Concurrency.Start(arg00);
      };
      return f5(x);
     },
     incrementQuestionsCount:function(x)
     {
      return Client4.incrementDataCount("#fsharpQuestions","data-questions-count",x);
     },
     makeQuestionLi:function(link,title,date,website,summary)
     {
      var _this,x,_this1,x1,x2,_this2;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("question")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([(x2=date+", "+website,Default.Text(x2))]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     questionsDiv:function()
     {
      var questionsList,loadMoreBtn,x,f,x1,x7,_this,arg003,_this1,arg004,f7,f8;
      questionsList=Default.UL(List.ofArray([Default.Id("questionsList")]));
      loadMoreBtn=(x=Default.Button(List.ofArray([Default.Text("Load More"),Default.Attr().Class("btn loadMore")])),(f=(x1=function(x2)
      {
       return function()
       {
        var x3,f1,f6;
        x3=(f1=function()
        {
         var objectArg,arg00,jquery,count,x4,f2,x5,f3;
         objectArg=x2["HtmlProvider@32"];
         ((arg00=x2.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         jquery=jQuery("#fsharpQuestions");
         count=(x4=jquery.attr("data-questions-count"),(f2=function(value)
         {
          return value<<0;
         },f2(x4)));
         x5=Remoting.Async("Website:5",[count]);
         f3=function(_arg11)
         {
          var x6,f4,mapping,f5,action,_count_,objectArg1,arg002;
          x6=(f4=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var link,title,date,website,summary;
           link=tupledArg[0];
           title=tupledArg[1];
           date=tupledArg[2];
           website=tupledArg[3];
           summary=tupledArg[4];
           return Client3.makeQuestionLi(link,title,date,website,summary);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(_arg11));
          f5=(action=function(arg001)
          {
           return questionsList.AppendI(arg001);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x6);
          _count_=_arg11.length;
          Client3.incrementQuestionsCount(_count_);
          objectArg1=x2["HtmlProvider@32"];
          (arg002=x2.Body,function(arg10)
          {
           return objectArg1.RemoveAttribute(arg002,arg10);
          })("disabled");
          return Concurrency.Return(null);
         };
         return Concurrency.Bind(x5,f3);
        },Concurrency.Delay(f1));
        f6=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f6(x3);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x)));
      x7=Operators.add(Default.Div(List.ofArray([Default.Id("fsharpQuestions"),(_this=HTML5.Attr(),(arg003="data-"+"questions-count",_this.NewAttr(arg003,"0"))),(_this1=HTML5.Attr(),(arg004="data-"+"question-id",_this1.NewAttr(arg004,"")))])),List.ofArray([questionsList,loadMoreBtn]));
      f7=(f8=function()
      {
       var x2,f1,f6;
       x2=(f1=function()
       {
        var x3,f2;
        x3=Remoting.Async("Website:4",[]);
        f2=Runtime.Tupled(function(_arg21)
        {
         var id,fsharpQuestions,x4,f3,mapping,f4,action,objectArg,arg00,x6,f5;
         id=_arg21[0];
         fsharpQuestions=_arg21[1];
         x4=(f3=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var link,title,date,website,summary;
          link=tupledArg[0];
          title=tupledArg[1];
          date=tupledArg[2];
          website=tupledArg[3];
          summary=tupledArg[4];
          return Client3.makeQuestionLi(link,title,date,website,summary);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f3(fsharpQuestions));
         f4=(action=function(x5)
         {
          return questionsList.AppendI(x5);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f4(x4);
         Client3.incrementQuestionsCount(20);
         Client3.setQuestionId(id);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         x6=setInterval(function()
         {
          return Client3.checkNewQuestions();
         },420000);
         f5=function(value)
         {
          value;
         };
         f5(x6);
         return Concurrency.Return(null);
        });
        return Concurrency.Bind(x3,f2);
       },Concurrency.Delay(f1));
       f6=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f6(x2);
      },function(w)
      {
       return Operators.OnAfterRender(f8,w);
      });
      f7(x7);
      return x7;
     },
     setQuestionId:function(id)
     {
      return Client4.setAttributeValue("#fsharpQuestions","data-question-id",id);
     }
    }
   },
   Snippets:{
    Client:{
     SnippetsViewer:Runtime.Class({
      get_Body:function()
      {
       return Client5.snippetsDiv();
      }
     }),
     incrementSnippetsCount:function(x)
     {
      return Client4.incrementDataCount("#fsharpSnippets","data-snippets-count",x);
     },
     makeSnippetLi:function(link,title,description)
     {
      var _this,x,_this1;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("snippet")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),Default.P(List.ofArray([Default.Text(description)]))]));
     },
     snippetsDiv:function()
     {
      var snippetsList,loadMoreBtn,x,f,x1,x7,_this,arg003,f7,f8;
      snippetsList=Default.UL(List.ofArray([Default.Id("snippetsList")]));
      loadMoreBtn=(x=Default.Button(List.ofArray([Default.Text("Load More"),Default.Attr().Class("btn loadMore")])),(f=(x1=function(x2)
      {
       return function()
       {
        var x3,f1,f6;
        x3=(f1=function()
        {
         var objectArg,arg00,jquery,count,x4,f2,x5,f3;
         objectArg=x2["HtmlProvider@32"];
         ((arg00=x2.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         jquery=jQuery("#fsharpSnippets");
         count=(x4=jquery.attr("data-snippets-count"),(f2=function(value)
         {
          return value<<0;
         },f2(x4)));
         x5=Remoting.Async("Website:11",[count]);
         f3=function(_arg11)
         {
          var x6,f4,mapping,f5,action,_count_,objectArg1,arg002;
          x6=(f4=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var link,title,description;
           link=tupledArg[0];
           title=tupledArg[1];
           description=tupledArg[2];
           return Client5.makeSnippetLi(link,title,description);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(_arg11));
          f5=(action=function(arg001)
          {
           return snippetsList.AppendI(arg001);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x6);
          _count_=_arg11.length;
          Client5.incrementSnippetsCount(_count_);
          objectArg1=x2["HtmlProvider@32"];
          (arg002=x2.Body,function(arg10)
          {
           return objectArg1.RemoveAttribute(arg002,arg10);
          })("disabled");
          return Concurrency.Return(null);
         };
         return Concurrency.Bind(x5,f3);
        },Concurrency.Delay(f1));
        f6=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f6(x3);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x)));
      x7=Operators.add(Default.Div(List.ofArray([Default.Id("fsharpSnippets"),(_this=HTML5.Attr(),(arg003="data-"+"snippets-count",_this.NewAttr(arg003,"0")))])),List.ofArray([snippetsList]));
      f7=(f8=function()
      {
       var x2,f1,f5;
       x2=(f1=function()
       {
        var x3,f2;
        x3=Remoting.Async("Website:10",[]);
        f2=function(_arg21)
        {
         var x4,f3,mapping,f4,action;
         x4=(f3=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var link,title,description;
          link=tupledArg[0];
          title=tupledArg[1];
          description=tupledArg[2];
          return Client5.makeSnippetLi(link,title,description);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f3(_arg21));
         f4=(action=function(x5)
         {
          return snippetsList.AppendI(x5);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f4(x4);
         Client5.incrementSnippetsCount(20);
         return Concurrency.Return(null);
        };
        return Concurrency.Bind(x3,f2);
       },Concurrency.Delay(f1));
       f5=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f5(x2);
      },function(w)
      {
       return Operators.OnAfterRender(f8,w);
      });
      f7(x7);
      return x7;
     }
    }
   },
   Tweets:{
    Client:{
     checkNewTweets:function()
     {
      var x,f,f6;
      x=(f=function()
      {
       var jquery,latestTweetId,x1,f1;
       jquery=jQuery("#fsharpTweets");
       latestTweetId=jquery.attr("data-tweet-id");
       x1=Remoting.Async("Website:9",[latestTweetId]);
       f1=function(_arg1)
       {
        var tweets,latestTweetId1,x2,f2,x3,x4,f3,f4,mapping,f5,action,count,msg;
        if(_arg1.$==1)
         {
          tweets=_arg1.$0;
          latestTweetId1=(x2=tweets[0],(f2=Runtime.Tupled(function(tupledArg)
          {
           var _arg11,id,_arg2,_arg3,_arg4,_arg5;
           _arg11=tupledArg[0];
           id=tupledArg[1];
           _arg2=tupledArg[2];
           _arg3=tupledArg[3];
           _arg4=tupledArg[4];
           _arg5=tupledArg[5];
           return id;
          }),f2(x2)));
          x3=(x4=(f3=function(array)
          {
           return array.slice(0,array.length).reverse();
          },f3(tweets)),(f4=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var screenName,tweetId,profileImage,displayName,text,creationDate;
           screenName=tupledArg[0];
           tweetId=tupledArg[1];
           profileImage=tupledArg[2];
           displayName=tupledArg[3];
           text=tupledArg[4];
           creationDate=tupledArg[5];
           return Client6.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(x4)));
          f5=(action=function(element)
          {
           return Client4.prependElement("#tweetsList",element);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x3);
          count=tweets.length;
          Client6.incrementTweetsCount(count);
          Client6.setTweetId(latestTweetId1);
          Client6.toggleActionsVisibility();
          msg=count===1?"1 new tweet":Global.String(count)+" new tweets";
          Client4.displayInfoAlert(msg);
          return Concurrency.Return(null);
         }
        else
         {
          return Concurrency.Return(null);
         }
       };
       return Concurrency.Bind(x1,f1);
      },Concurrency.Delay(f));
      f6=function(arg00)
      {
       var t;
       t={
        $:0
       };
       return Concurrency.Start(arg00);
      };
      return f6(x);
     },
     handleTweetActions:function()
     {
      var jquery;
      jquery=jQuery(".tweet-action-link");
      return jquery.mousedown(function(event)
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
     incrementTweetsCount:function(x)
     {
      return Client4.incrementDataCount("#fsharpTweets","data-tweets-count",x);
     },
     makeTweetLi:function(screenName,tweetId,profileImage,fullName,tweetHtml,creationDate)
     {
      var profileLink,replyLink,retweetLink,favoriteLink,tweetP,_this,x,_this1,x1,_this2,_this3,_this4,_this5;
      profileLink="https://twitter.com/"+screenName;
      replyLink="https://twitter.com/intent/tweet?in_reply_to="+tweetId;
      retweetLink="https://twitter.com/intent/retweet?tweet_id="+tweetId;
      favoriteLink="https://twitter.com/intent/retweet?tweet_id="+tweetId;
      tweetP=Default.P(Runtime.New(T,{
       $:0
      }));
      tweetP.set_Html(tweetHtml);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweet")])),List.ofArray([Operators.add(Operators.add(Default.A(List.ofArray([Default.HRef(profileLink),Default.Attr().Class("twitterProfileLink"),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([Default.Img(List.ofArray([Default.Src(profileImage),Default.Alt(fullName),Default.Attr().Class("avatar"),Default.Height("48"),Default.Width("48")])),(x=List.ofArray([Default.Text(fullName)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),List.ofArray([Default.Text(" @"+screenName)])),Default.Br(Runtime.New(T,{
       $:0
      })),(x1=List.ofArray([Default.Text(creationDate)]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),tweetP,Operators.add(Default.Div(List.ofArray([Default.Attr().Class("pull-right")])),List.ofArray([Operators.add(Default.UL(List.ofArray([Default.Attr().Class("tweetActions")])),List.ofArray([Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(replyLink),(_this3=Default.Attr(),_this3.NewAttr("target","_blank")),Default.Attr().Class("tweet-action-link")])),List.ofArray([Default.Text("Reply")]))])),Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(retweetLink),(_this4=Default.Attr(),_this4.NewAttr("target","_blank")),Default.Attr().Class("tweet-action-link")])),List.ofArray([Default.Text("Retweet")]))])),Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(favoriteLink),(_this5=Default.Attr(),_this5.NewAttr("target","_blank")),Default.Attr().Class("tweet-action-link")])),List.ofArray([Default.Text("Favorite")]))]))]))]))]));
     },
     setTweetId:function(id)
     {
      return Client4.setAttributeValue("#fsharpTweets","data-tweet-id",id);
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
     tweetsDiv:function()
     {
      var tweetsList,loadMoreBtn,x,f,x1,x7,_this,arg003,_this1,arg004,f7,f8;
      tweetsList=Default.UL(List.ofArray([Default.Id("tweetsList")]));
      loadMoreBtn=(x=Default.Button(List.ofArray([Default.Text("Load More"),Default.Attr().Class("btn loadMore")])),(f=(x1=function(x2)
      {
       return function()
       {
        var x3,f1,f6;
        x3=(f1=function()
        {
         var objectArg,arg00,jquery,count,x4,f2,x5,f3;
         objectArg=x2["HtmlProvider@32"];
         ((arg00=x2.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         jquery=jQuery("#fsharpTweets");
         count=(x4=jquery.attr("data-tweets-count"),(f2=function(value)
         {
          return value<<0;
         },f2(x4)));
         x5=Remoting.Async("Website:8",[count]);
         f3=function(_arg11)
         {
          var x6,f4,mapping,f5,action,_count_,objectArg1,arg002;
          x6=(f4=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var screenName,tweetId,profileImage,displayName,text,creationDate;
           screenName=tupledArg[0];
           tweetId=tupledArg[1];
           profileImage=tupledArg[2];
           displayName=tupledArg[3];
           text=tupledArg[4];
           creationDate=tupledArg[5];
           return Client6.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(_arg11));
          f5=(action=function(arg001)
          {
           return tweetsList.AppendI(arg001);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x6);
          _count_=_arg11.length;
          Client6.incrementTweetsCount(_count_);
          Client6.toggleActionsVisibility();
          Client6.handleTweetActions();
          objectArg1=x2["HtmlProvider@32"];
          (arg002=x2.Body,function(arg10)
          {
           return objectArg1.RemoveAttribute(arg002,arg10);
          })("disabled");
          return Concurrency.Return(null);
         };
         return Concurrency.Bind(x5,f3);
        },Concurrency.Delay(f1));
        f6=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f6(x3);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x)));
      x7=Operators.add(Default.Div(List.ofArray([Default.Id("fsharpTweets"),(_this=HTML5.Attr(),(arg003="data-"+"tweets-count",_this.NewAttr(arg003,"0"))),(_this1=HTML5.Attr(),(arg004="data-"+"tweet-id",_this1.NewAttr(arg004,"")))])),List.ofArray([tweetsList,loadMoreBtn]));
      f7=(f8=function()
      {
       var x2,f1,f9;
       x2=(f1=function()
       {
        var x3,f2;
        x3=Remoting.Async("Website:7",[]);
        f2=function(_arg2)
        {
         var latestTweetId,x4,f3,x5,f4,mapping,f5,action,objectArg,arg00,x8,f6;
         latestTweetId=(x4=_arg2[0],(f3=Runtime.Tupled(function(tupledArg)
         {
          var _arg21,id,_arg3,_arg4,_arg5,_arg6;
          _arg21=tupledArg[0];
          id=tupledArg[1];
          _arg3=tupledArg[2];
          _arg4=tupledArg[3];
          _arg5=tupledArg[4];
          _arg6=tupledArg[5];
          return id;
         }),f3(x4)));
         x5=(f4=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var screenName,tweetId,profileImage,displayName,text,creationDate;
          screenName=tupledArg[0];
          tweetId=tupledArg[1];
          profileImage=tupledArg[2];
          displayName=tupledArg[3];
          text=tupledArg[4];
          creationDate=tupledArg[5];
          return Client6.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f4(_arg2));
         f5=(action=function(x6)
         {
          return tweetsList.AppendI(x6);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f5(x5);
         Client6.incrementTweetsCount(20);
         Client6.setTweetId(latestTweetId);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         Client6.toggleActionsVisibility();
         Client6.handleTweetActions();
         x8=setInterval(function()
         {
          return Client6.checkNewTweets();
         },300000);
         f6=function(value)
         {
          value;
         };
         f6(x8);
         return Concurrency.Return(null);
        };
        return Concurrency.Bind(x3,f2);
       },Concurrency.Delay(f1));
       f9=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f9(x2);
      },function(w)
      {
       return Operators.OnAfterRender(f8,w);
      });
      f7(x7);
      return x7;
     }
    },
    FsharpTweetsViewer:Runtime.Class({
     get_Body:function()
     {
      return Client6.tweetsDiv();
     }
    })
   },
   Utilities:{
    Client:{
     displayInfoAlert:function(msg)
     {
      jQuery("#alertText").text(msg);
      jQuery("#alertDiv").show(600);
      return jQuery("#alertDiv").fadeOut(7000);
     },
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
  Forkme=Runtime.Safe(Website.Forkme);
  Login=Runtime.Safe(Website.Login);
  Client1=Runtime.Safe(Login.Client);
  window=Runtime.Safe(Global.window);
  NewsAdmin=Runtime.Safe(Website.NewsAdmin);
  Client2=Runtime.Safe(NewsAdmin.Client);
  Questions=Runtime.Safe(Website.Questions);
  Client3=Runtime.Safe(Questions.Client);
  jQuery=Runtime.Safe(Global.jQuery);
  Utilities=Runtime.Safe(Website.Utilities);
  Client4=Runtime.Safe(Utilities.Client);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  setInterval=Runtime.Safe(Global.setInterval);
  Snippets=Runtime.Safe(Website.Snippets);
  Client5=Runtime.Safe(Snippets.Client);
  Tweets=Runtime.Safe(Website.Tweets);
  Client6=Runtime.Safe(Tweets.Client);
  Videos=Runtime.Safe(Website.Videos);
  Client7=Runtime.Safe(Videos.Client);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client8=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  Client.booksTable();
  Client.addFormlet();
 });
}());
