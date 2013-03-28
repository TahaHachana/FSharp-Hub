(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Website,BooksAdmin,Client,WebSharper,Html,Operators,Default,List,Remoting,Arrays,Concurrency,Forkme,Login,Client1,Formlet,Controls,Enhance,Data,Formlet1,window,alert,News,Client2,Questions,Client3,jQuery,Utilities,Client4,T,EventsPervasives,HTML5,setInterval,Snippets,Client5,Tweets,Client6,Videos,Client7;
 Runtime.Define(Global,{
  Website:{
   BooksAdmin:{
    Client:{
     Control:Runtime.Class({
      get_Body:function()
      {
       return Client.main();
      }
     }),
     main:function()
     {
      var x,f,f1;
      x=Operators.add(Default.Table(List.ofArray([Default.Attr().Class("table")])),List.ofArray([Default.TR(List.ofArray([Default.TH(List.ofArray([Default.Text("Title")])),Default.TH(List.ofArray([Default.Text("Publisher")]))]))]));
      f=(f1=function(elt)
      {
       var x1,f2,f6;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:0",[]);
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
     return Operators.add(Default.A(List.ofArray([Default.HRef("https://github.com/TahaHachana/FSharpWebsite")])),List.ofArray([Default.Img(List.ofArray([Default.Src("https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"),Default.Alt("Fork me on GitHub"),Default.Id("forkme")]))]));
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
        x6=Remoting.Async("Website:10",[loginInfo]);
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
   News:{
    Client:{
     NewsViewer:Runtime.Class({
      get_Body:function()
      {
       return Client2.newsList();
      }
     }),
     makeLi:function(title,url,summary)
     {
      var x,_this;
      return Default.LI(List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(url)])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this=Default.Tags(),_this.NewTag("strong",x)))])),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     newsList:function()
     {
      var x,f,f1;
      x=Default.UL(List.ofArray([Default.Attr().Class("unstyled"),Default.Id("newsList")]));
      f=(f1=function(ul)
      {
       var x1,f2,f6;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:1",[]);
        f3=function(_arg1)
        {
         var x3,f4,f5,action;
         x3=(f4=function(array)
         {
          return Arrays.map(Runtime.Tupled(function(tupledArg)
          {
           var title,url,summary;
           title=tupledArg[0];
           url=tupledArg[1];
           summary=tupledArg[2];
           return Client2.makeLi(title,url,summary);
          }),array);
         },f4(_arg1));
         f5=(action=function(x4)
         {
          return ul.AppendI(x4);
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
       x1=Remoting.Async("Website:4",[latestQuestionId]);
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
         x5=Remoting.Async("Website:3",[count]);
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
        x3=Remoting.Async("Website:2",[]);
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
         x5=Remoting.Async("Website:9",[count]);
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
        x3=Remoting.Async("Website:8",[]);
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
       x1=Remoting.Async("Website:7",[latestTweetId]);
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
         x5=Remoting.Async("Website:6",[count]);
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
        x3=Remoting.Async("Website:5",[]);
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
   }
  }
 });
 Runtime.OnInit(function()
 {
  Website=Runtime.Safe(Global.Website);
  BooksAdmin=Runtime.Safe(Website.BooksAdmin);
  Client=Runtime.Safe(BooksAdmin.Client);
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Operators=Runtime.Safe(Html.Operators);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Forkme=Runtime.Safe(Website.Forkme);
  Login=Runtime.Safe(Website.Login);
  Client1=Runtime.Safe(Login.Client);
  Formlet=Runtime.Safe(WebSharper.Formlet);
  Controls=Runtime.Safe(Formlet.Controls);
  Enhance=Runtime.Safe(Formlet.Enhance);
  Data=Runtime.Safe(Formlet.Data);
  Formlet1=Runtime.Safe(Formlet.Formlet);
  window=Runtime.Safe(Global.window);
  alert=Runtime.Safe(Global.alert);
  News=Runtime.Safe(Website.News);
  Client2=Runtime.Safe(News.Client);
  Questions=Runtime.Safe(Website.Questions);
  Client3=Runtime.Safe(Questions.Client);
  jQuery=Runtime.Safe(Global.jQuery);
  Utilities=Runtime.Safe(Website.Utilities);
  Client4=Runtime.Safe(Utilities.Client);
  T=Runtime.Safe(List.T);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  HTML5=Runtime.Safe(Default.HTML5);
  setInterval=Runtime.Safe(Global.setInterval);
  Snippets=Runtime.Safe(Website.Snippets);
  Client5=Runtime.Safe(Snippets.Client);
  Tweets=Runtime.Safe(Website.Tweets);
  Client6=Runtime.Safe(Tweets.Client);
  Videos=Runtime.Safe(Website.Videos);
  return Client7=Runtime.Safe(Videos.Client);
 });
 Runtime.OnLoad(function()
 {
 });
}());
