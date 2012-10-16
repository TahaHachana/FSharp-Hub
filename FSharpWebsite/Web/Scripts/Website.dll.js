(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,jQuery,WebSharper,Remoting,FSharpWebsite,FSharpQuestions,Client,Arrays,Utilities,Concurrency,Html,Operators,Default,List,setInterval,EventsPervasives,FSharpTweets,Client1,T;
 Runtime.Define(Global,{
  FSharpWebsite:{
   FSharpQuestions:{
    Client:{
     checkNewQuestions:function()
     {
      var x,f,f5;
      x=(f=function()
      {
       var jquery,latestQuestionId,x1,f1;
       jquery=jQuery("#fsharpQuestions");
       latestQuestionId=jquery.attr("data-question-id");
       x1=Remoting.Async("Website:2",[latestQuestionId]);
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
           return Client.makeQuestionLi(link,title,date,website,summary);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f3(x3)));
          f4=(action=function(x4)
          {
           return jQuery("#questionsList").prepend(x4.Body);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f4(x2);
          count=questions.length;
          Client.incrementQuestionsCount(count);
          Client.setQuestionId(id);
          msg=count===1?"1 new question":Global.String(count)+" new question";
          Utilities.displayInfoAlert(msg);
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
      var jquery,count,x1,f,_count_,x2,f1;
      jquery=jQuery("#fsharpQuestions");
      count=(x1=jquery.attr("data-questions-count"),(f=function(value)
      {
       return value<<0;
      },f(x1)));
      _count_=(x2=x+count,(f1=function(value)
      {
       return Global.String(value);
      },f1(x2)));
      return jquery.attr("data-questions-count",_count_);
     },
     makeQuestionLi:function(link,title,date,website,summary)
     {
      var _this,x,_this1,x1,x2,_this2;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("question")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),(x1=List.ofArray([(x2=" ("+date+", "+website+")",Default.Text(x2))]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     questionsDiv:function()
     {
      var x,f,questionsList,loadMoreBtn,x1,f1,x2,x8,f8,f9;
      x=setInterval(function(_arg00_)
      {
       _arg00_;
       return Client.checkNewQuestions();
      },300000);
      f=function(value)
      {
       value;
      };
      f(x);
      questionsList=Default.UL(List.ofArray([Default.Id("questionsList")]));
      loadMoreBtn=(x1=Default.Button(List.ofArray([Default.Text("Load More"),Default.Attr().Class("btn loadMore")])),(f1=(x2=function(x3)
      {
       return function()
       {
        var x4,f2,f7;
        x4=(f2=function()
        {
         var objectArg,arg00,jquery,count,x5,f3,x6,f4;
         objectArg=x3["HtmlProvider@32"];
         ((arg00=x3.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         jquery=jQuery("#fsharpQuestions");
         count=(x5=jquery.attr("data-questions-count"),(f3=function(value)
         {
          return value<<0;
         },f3(x5)));
         x6=Remoting.Async("Website:1",[count]);
         f4=function(_arg11)
         {
          var x7,f5,mapping,f6,action,_count_,objectArg1,arg002;
          x7=(f5=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var link,title,date,website,summary;
           link=tupledArg[0];
           title=tupledArg[1];
           date=tupledArg[2];
           website=tupledArg[3];
           summary=tupledArg[4];
           return Client.makeQuestionLi(link,title,date,website,summary);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f5(_arg11));
          f6=(action=function(arg001)
          {
           return questionsList.AppendI(arg001);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f6(x7);
          _count_=_arg11.length;
          Client.incrementQuestionsCount(_count_);
          objectArg1=x3["HtmlProvider@32"];
          (arg002=x3.Body,function(arg10)
          {
           return objectArg1.RemoveAttribute(arg002,arg10);
          })("disabled");
          return Concurrency.Return(null);
         };
         return Concurrency.Bind(x6,f4);
        },Concurrency.Delay(f2));
        f7=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f7(x4);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x2,arg10);
      }),(f1(x1),x1)));
      x8=Operators.add(Default.Div(List.ofArray([Default.Id("questionsDiv")])),List.ofArray([questionsList,loadMoreBtn]));
      f8=(f9=function()
      {
       var x3,f2,f6;
       x3=(f2=function()
       {
        var x4,f3;
        x4=Remoting.Async("Website:0",[]);
        f3=Runtime.Tupled(function(_arg21)
        {
         var id,fsharpQuestions,x5,f4,mapping,f5,action,objectArg,arg00;
         id=_arg21[0];
         fsharpQuestions=_arg21[1];
         x5=(f4=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var link,title,date,website,summary;
          link=tupledArg[0];
          title=tupledArg[1];
          date=tupledArg[2];
          website=tupledArg[3];
          summary=tupledArg[4];
          return Client.makeQuestionLi(link,title,date,website,summary);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f4(fsharpQuestions));
         f5=(action=function(x6)
         {
          return questionsList.AppendI(x6);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f5(x5);
         Client.incrementQuestionsCount(20);
         Client.setQuestionId(id);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         return Concurrency.Return(null);
        });
        return Concurrency.Bind(x4,f3);
       },Concurrency.Delay(f2));
       f6=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f6(x3);
      },function(w)
      {
       return Operators.OnAfterRender(f9,w);
      });
      f8(x8);
      return x8;
     },
     setQuestionId:function(id)
     {
      return jQuery("#fsharpQuestions").attr("data-question-id",id);
     }
    },
    FsharpQuestionsViewer:Runtime.Class({
     get_Body:function()
     {
      return Client.questionsDiv();
     }
    })
   },
   FSharpTweets:{
    Client:{
     checkNewTweets:function()
     {
      var x,f,f6;
      x=(f=function()
      {
       var jquery,latestTweetId,x1,f1;
       jquery=jQuery("#fsharpTweets");
       latestTweetId=jquery.attr("data-tweet-id");
       x1=Remoting.Async("Website:5",[latestTweetId]);
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
           return Client1.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(x4)));
          f5=(action=function(x5)
          {
           return jQuery("#tweetsList").prepend(x5.Body);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x3);
          count=tweets.length;
          Client1.incrementTweetsCount(count);
          Client1.setTweetId(latestTweetId1);
          Client1.toggleActionsVisibility();
          msg=count===1?"1 new tweet":Global.String(count)+" new tweets";
          Utilities.displayInfoAlert(msg);
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
     incrementTweetsCount:function(x)
     {
      var jquery,count,x1,f,_count_,x2,f1;
      jquery=jQuery("#fsharpTweets");
      count=(x1=jquery.attr("data-tweets-count"),(f=function(value)
      {
       return value<<0;
      },f(x1)));
      _count_=(x2=x+count,(f1=function(value)
      {
       return Global.String(value);
      },f1(x2)));
      return jquery.attr("data-tweets-count",_count_);
     },
     makeTweetLi:function(screenName,tweetId,profileImage,fullName,tweetHtml,creationDate)
     {
      var profileLink,replyLink,retweetLink,favoriteLink,tweetP,_this,x,_this1,x1,_this2,x2,_this3,_this4,_this5;
      profileLink="https://twitter.com/"+screenName;
      replyLink="https://twitter.com/intent/tweet?in_reply_to="+tweetId;
      retweetLink="https://twitter.com/intent/retweet?tweet_id="+tweetId;
      favoriteLink="https://twitter.com/intent/retweet?tweet_id="+tweetId;
      tweetP=Default.P(Runtime.New(T,{
       $:0
      }));
      tweetP.set_Html(tweetHtml);
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweet")])),List.ofArray([Operators.add(Operators.add(Default.A(List.ofArray([Default.HRef(profileLink),Default.Attr().Class("twitterProfileLink"),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([Default.Img(List.ofArray([Default.Src(profileImage),Default.Alt(fullName),Default.Attr().Class("avatar"),Default.Height("48"),Default.Width("48")])),(x=List.ofArray([Default.Text(fullName)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),List.ofArray([Default.Text(" @"+screenName)])),Operators.add((x1=List.ofArray([Default.Attr().Class("pull-right")]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),List.ofArray([(x2="("+creationDate+")",Default.Text(x2))])),tweetP,Operators.add(Default.Div(List.ofArray([Default.Attr().Class("pull-right")])),List.ofArray([Operators.add(Default.UL(List.ofArray([Default.Attr().Class("tweetActions")])),List.ofArray([Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(replyLink),(_this3=Default.Attr(),_this3.NewAttr("target","_blank"))])),List.ofArray([Default.Text("Reply")]))])),Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(retweetLink),(_this4=Default.Attr(),_this4.NewAttr("target","_blank"))])),List.ofArray([Default.Text("Retweet")]))])),Operators.add(Default.LI(List.ofArray([Default.Attr().Class("tweetAction")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(favoriteLink),(_this5=Default.Attr(),_this5.NewAttr("target","_blank"))])),List.ofArray([Default.Text("Favorite")]))]))]))]))]));
     },
     setTweetId:function(id)
     {
      return jQuery("#fsharpTweets").attr("data-tweet-id",id);
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
      var tweetsList,x,f,loadMoreBtn,x1,f1,x2,x8,f8,f9;
      tweetsList=Default.UL(List.ofArray([Default.Id("tweetsList")]));
      x=setInterval(function(_arg00_)
      {
       _arg00_;
       return Client1.checkNewTweets();
      },300000);
      f=function(value)
      {
       value;
      };
      f(x);
      loadMoreBtn=(x1=Default.Button(List.ofArray([Default.Text("Load More"),Default.Attr().Class("btn loadMore")])),(f1=(x2=function(x3)
      {
       return function()
       {
        var x4,f2,f7;
        x4=(f2=function()
        {
         var objectArg,arg00,jquery,count,x5,f3,x6,f4;
         objectArg=x3["HtmlProvider@32"];
         ((arg00=x3.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         jquery=jQuery("#fsharpTweets");
         count=(x5=jquery.attr("data-tweets-count"),(f3=function(value)
         {
          return value<<0;
         },f3(x5)));
         x6=Remoting.Async("Website:4",[count]);
         f4=function(_arg11)
         {
          var x7,f5,mapping,f6,action,_count_,objectArg1,arg002;
          x7=(f5=(mapping=Runtime.Tupled(function(tupledArg)
          {
           var screenName,tweetId,profileImage,displayName,text,creationDate;
           screenName=tupledArg[0];
           tweetId=tupledArg[1];
           profileImage=tupledArg[2];
           displayName=tupledArg[3];
           text=tupledArg[4];
           creationDate=tupledArg[5];
           return Client1.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f5(_arg11));
          f6=(action=function(arg001)
          {
           return tweetsList.AppendI(arg001);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f6(x7);
          _count_=_arg11.length;
          Client1.incrementTweetsCount(_count_);
          Client1.toggleActionsVisibility();
          objectArg1=x3["HtmlProvider@32"];
          (arg002=x3.Body,function(arg10)
          {
           return objectArg1.RemoveAttribute(arg002,arg10);
          })("disabled");
          return Concurrency.Return(null);
         };
         return Concurrency.Bind(x6,f4);
        },Concurrency.Delay(f2));
        f7=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f7(x4);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x2,arg10);
      }),(f1(x1),x1)));
      x8=Operators.add(Default.Div(List.ofArray([Default.Id("tweetsDiv")])),List.ofArray([tweetsList,loadMoreBtn]));
      f8=(f9=function()
      {
       var x3,f2,f7;
       x3=(f2=function()
       {
        var x4,f3;
        x4=Remoting.Async("Website:3",[]);
        f3=function(_arg2)
        {
         var latestTweetId,x5,f4,x6,f5,mapping,f6,action,objectArg,arg00;
         latestTweetId=(x5=_arg2[0],(f4=Runtime.Tupled(function(tupledArg)
         {
          var _arg21,id,_arg3,_arg4,_arg5,_arg6;
          _arg21=tupledArg[0];
          id=tupledArg[1];
          _arg3=tupledArg[2];
          _arg4=tupledArg[3];
          _arg5=tupledArg[4];
          _arg6=tupledArg[5];
          return id;
         }),f4(x5)));
         x6=(f5=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var screenName,tweetId,profileImage,displayName,text,creationDate;
          screenName=tupledArg[0];
          tweetId=tupledArg[1];
          profileImage=tupledArg[2];
          displayName=tupledArg[3];
          text=tupledArg[4];
          creationDate=tupledArg[5];
          return Client1.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
         }),function(array)
         {
          return Arrays.map(mapping,array);
         }),f5(_arg2));
         f6=(action=function(x7)
         {
          return tweetsList.AppendI(x7);
         },function(array)
         {
          return Arrays.iter(action,array);
         });
         f6(x6);
         Client1.incrementTweetsCount(20);
         Client1.setTweetId(latestTweetId);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         Client1.toggleActionsVisibility();
         return Concurrency.Return(null);
        };
        return Concurrency.Bind(x4,f3);
       },Concurrency.Delay(f2));
       f7=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f7(x3);
      },function(w)
      {
       return Operators.OnAfterRender(f9,w);
      });
      f8(x8);
      return x8;
     }
    },
    FsharpTweetsViewer:Runtime.Class({
     get_Body:function()
     {
      return Client1.tweetsDiv();
     }
    })
   },
   Utilities:{
    displayInfoAlert:function(msg)
    {
     jQuery("#alertText").text(msg);
     jQuery("#alertDiv").show(600);
     return jQuery("#alertDiv").fadeOut(7000);
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  jQuery=Runtime.Safe(Global.jQuery);
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  FSharpWebsite=Runtime.Safe(Global.FSharpWebsite);
  FSharpQuestions=Runtime.Safe(FSharpWebsite.FSharpQuestions);
  Client=Runtime.Safe(FSharpQuestions.Client);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Utilities=Runtime.Safe(FSharpWebsite.Utilities);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Html=Runtime.Safe(WebSharper.Html);
  Operators=Runtime.Safe(Html.Operators);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  setInterval=Runtime.Safe(Global.setInterval);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  FSharpTweets=Runtime.Safe(FSharpWebsite.FSharpTweets);
  Client1=Runtime.Safe(FSharpTweets.Client);
  return T=Runtime.Safe(List.T);
 });
 Runtime.OnLoad(function()
 {
 });
}());
