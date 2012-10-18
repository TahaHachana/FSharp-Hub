(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Html,Operators,Default,List,T,Remoting,FSharpWebsite,FSharpBooks,Client,Seq,Concurrency,Strings,jQuery,FSharpQuestions,Client1,Arrays,Utilities,EventsPervasives,setInterval,FSharpTweets,Client2;
 Runtime.Define(Global,{
  FSharpWebsite:{
   FSharpBooks:{
    Client:{
     booksDiv:function()
     {
      var makeBooksUl,makeDiv,x,f,f1;
      makeBooksUl=function(li,_li_,_li__)
      {
       return Operators.add(Default.UL(List.ofArray([Default.Attr().Class("thumbnails")])),List.ofArray([li,_li_,_li__]));
      };
      makeDiv=function(ul)
      {
       return Operators.add(Default.Div(List.ofArray([Default.Attr().Class("row-fluid")])),List.ofArray([ul]));
      };
      x=Default.Div(Runtime.New(T,{
       $:0
      }));
      f=(f1=function(div)
      {
       var x1,f2,f8;
       x1=(f2=function()
       {
        var x2,f3;
        x2=Remoting.Async("Website:6",[]);
        f3=function(_arg1)
        {
         var x3,x4,x5,f4,mapping,f5,mapping1,f6,mapping2,f7,action;
         x3=(x4=(x5=(f4=(mapping=Runtime.Tupled(function(tupledArg)
         {
          var x6,y,z,li,_li_,_li__;
          x6=tupledArg[0];
          y=tupledArg[1];
          z=tupledArg[2];
          li=Client.makeThumbnailLi(x6[0],x6[1],x6[2],x6[3],x6[4],x6[5],x6[6]);
          _li_=Client.makeThumbnailLi(y[0],y[1],y[2],y[3],y[4],y[5],y[6]);
          _li__=Client.makeThumbnailLi(z[0],z[1],z[2],z[3],z[4],z[5],z[6]);
          return[li,_li_,_li__];
         }),function(source)
         {
          return Seq.map(mapping,source);
         }),f4(_arg1)),(f5=(mapping1=Runtime.Tupled(function(tupledArg)
         {
          var x6,y,z;
          x6=tupledArg[0];
          y=tupledArg[1];
          z=tupledArg[2];
          return makeBooksUl(x6,y,z);
         }),function(source)
         {
          return Seq.map(mapping1,source);
         }),f5(x5))),(f6=(mapping2=makeDiv,function(source)
         {
          return Seq.map(mapping2,source);
         }),f6(x4)));
         f7=(action=function(arg00)
         {
          return div.AppendI(arg00);
         },function(source)
         {
          return Seq.iter(action,source);
         });
         f7(x3);
         return Concurrency.Return(null);
        };
        return Concurrency.Bind(x2,f3);
       },Concurrency.Delay(f2));
       f8=function(arg00)
       {
        var t;
        t={
         $:0
        };
        return Concurrency.Start(arg00);
       };
       return f8(x1);
      },function(w)
      {
       return Operators.OnAfterRender(f1,w);
      });
      f(x);
      return x;
     },
     makeThumbnailLi:function(url,cover,title,authors,publisher,isbn,pages)
     {
      var x,x1,x2,x3,_this;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("span4")])),List.ofArray([Operators.add(Default.Div(List.ofArray([Default.Attr().Class("thumbnail")])),List.ofArray([Default.Img(List.ofArray([Default.Src(cover),Default.Alt(title),Default.Width("180"),Default.Height("220")])),Default.H3(List.ofArray([Default.Text(title)])),Default.P(List.ofArray([(x="Authors: "+Strings.concat(", ",authors),Default.Text(x))])),Default.P(List.ofArray([(x1="Pbulisher: "+publisher,Default.Text(x1))])),Default.P(List.ofArray([(x2="ISBN: "+isbn,Default.Text(x2))])),Default.P(List.ofArray([(x3="Pages: "+pages,Default.Text(x3))])),Operators.add(Default.A(List.ofArray([Default.HRef(url),Default.Attr().Class("btn btn-primary"),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([Default.Text("Book Website")]))]))]));
     }
    },
    FsharpBooksViewer:Runtime.Class({
     get_Body:function()
     {
      return Client.booksDiv();
     }
    })
   },
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
           return Client1.makeQuestionLi(link,title,date,website,summary);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f3(x3)));
          f4=(action=function(element)
          {
           return Utilities.prependElement("#questionsList",element);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f4(x2);
          count=questions.length;
          Client1.incrementQuestionsCount(count);
          Client1.setQuestionId(id);
          msg=count===1?"1 new question":Global.String(count)+" new questions";
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
      return Utilities.incrementDataCount("#fsharpQuestions","data-questions-count",x);
     },
     makeQuestionLi:function(link,title,date,website,summary)
     {
      var _this,x,_this1,x1,x2,_this2;
      return Operators.add(Default.LI(List.ofArray([Default.Attr().Class("question")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef(link),(_this=Default.Attr(),_this.NewAttr("target","_blank"))])),List.ofArray([(x=List.ofArray([Default.Text(title)]),(_this1=Default.Tags(),_this1.NewTag("strong",x)))])),(x1=List.ofArray([(x2=" ("+date+", "+website+")",Default.Text(x2))]),(_this2=Default.Tags(),_this2.NewTag("small",x1))),Default.P(List.ofArray([Default.Text(summary)]))]));
     },
     questionsDiv:function()
     {
      var questionsList,loadMoreBtn,x,f,x1,x7,f7,f8;
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
         x5=Remoting.Async("Website:1",[count]);
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
           return Client1.makeQuestionLi(link,title,date,website,summary);
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
          Client1.incrementQuestionsCount(_count_);
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
      x7=Operators.add(Default.Div(List.ofArray([Default.Id("questionsDiv")])),List.ofArray([questionsList,loadMoreBtn]));
      f7=(f8=function()
      {
       var x2,f1,f6;
       x2=(f1=function()
       {
        var x3,f2;
        x3=Remoting.Async("Website:0",[]);
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
          return Client1.makeQuestionLi(link,title,date,website,summary);
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
         Client1.incrementQuestionsCount(20);
         Client1.setQuestionId(id);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         x6=setInterval(function(_arg00_)
         {
          _arg00_;
          return Client1.checkNewQuestions();
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
      return Utilities.setAttributeValue("#fsharpQuestions","data-question-id",id);
     }
    },
    FsharpQuestionsViewer:Runtime.Class({
     get_Body:function()
     {
      return Client1.questionsDiv();
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
           return Client2.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
          }),function(array)
          {
           return Arrays.map(mapping,array);
          }),f4(x4)));
          f5=(action=function(element)
          {
           return Utilities.prependElement("#tweetsList",element);
          },function(array)
          {
           return Arrays.iter(action,array);
          });
          f5(x3);
          count=tweets.length;
          Client2.incrementTweetsCount(count);
          Client2.setTweetId(latestTweetId1);
          Client2.toggleActionsVisibility();
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
      return Utilities.incrementDataCount("#fsharpTweets","data-tweets-count",x);
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
      return Utilities.setAttributeValue("#fsharpTweets","data-tweet-id",id);
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
      var tweetsList,loadMoreBtn,x,f,x1,x7,f7,f8;
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
         x5=Remoting.Async("Website:4",[count]);
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
           return Client2.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
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
          Client2.incrementTweetsCount(_count_);
          Client2.toggleActionsVisibility();
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
      x7=Operators.add(Default.Div(List.ofArray([Default.Id("tweetsDiv")])),List.ofArray([tweetsList,loadMoreBtn]));
      f7=(f8=function()
      {
       var x2,f1,f9;
       x2=(f1=function()
       {
        var x3,f2;
        x3=Remoting.Async("Website:3",[]);
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
          return Client2.makeTweetLi(screenName,tweetId,profileImage,displayName,text,creationDate);
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
         Client2.incrementTweetsCount(20);
         Client2.setTweetId(latestTweetId);
         objectArg=loadMoreBtn["HtmlProvider@32"];
         ((arg00=loadMoreBtn.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetCss(arg00,arg10,arg20);
          };
         })("visibility"))("visible");
         Client2.toggleActionsVisibility();
         x8=setInterval(function(_arg00_)
         {
          _arg00_;
          return Client2.checkNewTweets();
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
      return Client2.tweetsDiv();
     }
    })
   },
   Utilities:{
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
  }
 });
 Runtime.OnInit(function()
 {
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Operators=Runtime.Safe(Html.Operators);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  T=Runtime.Safe(List.T);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  FSharpWebsite=Runtime.Safe(Global.FSharpWebsite);
  FSharpBooks=Runtime.Safe(FSharpWebsite.FSharpBooks);
  Client=Runtime.Safe(FSharpBooks.Client);
  Seq=Runtime.Safe(WebSharper.Seq);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Strings=Runtime.Safe(WebSharper.Strings);
  jQuery=Runtime.Safe(Global.jQuery);
  FSharpQuestions=Runtime.Safe(FSharpWebsite.FSharpQuestions);
  Client1=Runtime.Safe(FSharpQuestions.Client);
  Arrays=Runtime.Safe(WebSharper.Arrays);
  Utilities=Runtime.Safe(FSharpWebsite.Utilities);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  setInterval=Runtime.Safe(Global.setInterval);
  FSharpTweets=Runtime.Safe(FSharpWebsite.FSharpTweets);
  return Client2=Runtime.Safe(FSharpTweets.Client);
 });
 Runtime.OnLoad(function()
 {
 });
}());
