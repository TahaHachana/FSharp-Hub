(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Formlet,Controls,Enhance,Data,Formlet1,Concurrency,Remoting,alert,Html,Operators,Default,List,Website,BooksAdmin,Client,Arrays,GitHubGists,Client1,Seq,GitHubIssues,Client2,GitHubRepos,Client3,HTML5,Login,Client4,window,EventsPervasives,jQuery,News,OperatorIntrinsics,Questions,Client5,T,Snippets,Client6,Utils,StackOverflow,Client7,Twitter,Client8,VideosAdmin,Client9;
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
        return Concurrency.Bind(Remoting.Async("Sitelet:11",[url,title,authors,publisher,isbn,pages,date,cover]),function(arg101)
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
   GitHubGists:{
    Client:{
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:4",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         var ul;
         ul=Default.UL(List.ofArray([Default.Attr().Class("media-list")]));
         Arrays.iter(function(gist)
         {
          var arg102,arg103,arg104;
          arg102=gist.ownerLink;
          arg103=gist.ownerAvatar;
          arg104=gist.link;
          return ul.AppendI(Operators.add(Default.LI(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",arg102),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("src",arg103)]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg104),Default.Attr().NewAttr("target","_blank"),Default.Text("Gist by "+gist.ownerName)]))]))]))])));
         },arg101);
         elt.AppendI(ul);
         return Concurrency.Return(null);
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
   GitHubIssues:{
    Client:{
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
        return Concurrency.Bind(x1,function(arg101)
        {
         var ul;
         ul=Default.UL(List.ofArray([Default.Attr().Class("media-list"),Default.Attr().NewAttr("id","tweets-ul")]));
         Seq.iter(Runtime.Tupled(function(tupledArg)
         {
          var userUrl,userAvatar,title,url,updatedAt;
          userUrl=tupledArg[0];
          userAvatar=tupledArg[1];
          title=tupledArg[2];
          url=tupledArg[3];
          updatedAt=tupledArg[4];
          return ul.AppendI(Operators.add(Default.LI(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",userUrl),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("style","width: 30px; height: 30px;"),Default.Attr().NewAttr("src",userAvatar)]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",url),Default.Attr().NewAttr("target","_blank"),Default.Text(title)]))])),Default.P(List.ofArray([Default.Text(updatedAt)]))]))])));
         }),arg101);
         elt.AppendI(ul);
         return Concurrency.Return(null);
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
   GitHubRepos:{
    Client:{
     newRepos:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:2",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         var ul;
         ul=Default.UL(List.ofArray([Default.Attr().Class("media-list"),Default.Attr().NewAttr("id","tweets-ul")]));
         Arrays.iter(function(repo)
         {
          var arg102,arg103,arg104;
          arg102=repo.ownerLink;
          arg103=repo.ownerAvatar;
          arg104=repo.link;
          return ul.AppendI(Operators.add(Default.LI(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",arg102),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("style","width: 30px; height: 30px;"),Default.Attr().NewAttr("src",arg103)]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg104),Default.Attr().NewAttr("target","_blank"),Default.Text(repo.name)]))])),Default.P(List.ofArray([Default.Text(repo.createdAt)]))]))])));
         },arg101);
         elt.AppendI(ul);
         return Concurrency.Return(null);
        });
       }));
      },x);
      return x;
     },
     updatedRepos:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:3",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         var ul;
         ul=Default.UL(List.ofArray([Default.Attr().Class("media-list"),Default.Attr().NewAttr("id","tweets-ul")]));
         Arrays.iter(function(repo)
         {
          var arg102,arg103,arg104;
          arg102=repo.ownerLink;
          arg103=repo.ownerAvatar;
          arg104=repo.link;
          return ul.AppendI(Operators.add(Default.LI(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",arg102),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("style","width: 30px; height: 30px;"),Default.Attr().NewAttr("src",arg103)]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg104),Default.Attr().NewAttr("target","_blank"),Default.Text(repo.name)]))])),Default.P(List.ofArray([Default.Text(repo.pushedAt)]))]))])));
         },arg101);
         elt.AppendI(ul);
         return Concurrency.Return(null);
        });
       }));
      },x);
      return x;
     }
    },
    NewReposControl:Runtime.Class({
     get_Body:function()
     {
      return Client3.newRepos();
     }
    }),
    UpdatedReposControl:Runtime.Class({
     get_Body:function()
     {
      return Client3.updatedRepos();
     }
    })
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
         return Concurrency.Bind(Remoting.Async("Sitelet:9",[{
          Name:userInput.get_Value(),
          Password:Client4.passInput().get_Value()
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
      return Operators.add(Default.Form(List.ofArray([Default.Attr().NewAttr("role","form"),Default.Attr().NewAttr("id","signin")])),List.ofArray([Default.H2(List.ofArray([Default.Text("Please sign in")])),Operators.add(Default.Tags().NewTag("fieldset",arg102),List.ofArray([Default.Tags().NewTag("label",arg103),userInput,Default.Tags().NewTag("label",arg104),Client4.passInput()])),Default.Tags().NewTag("fieldset",arg105)]));
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
      return Client4.loginForm(this.redirectUrl);
     }
    })
   },
   News:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return News.main();
     }
    }),
    main:Runtime.Field(function()
    {
     var _ul_43_1;
     _ul_43_1=Default.UL(List.ofArray([Default.Attr().NewAttr("id","news-list")]));
     jQuery.getJSON("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&callback=?&q=http%3A%2F%2Ffpish.net%2Frss%2Fblogs%2Ftag%2F1%2Ff~23",Runtime.Tupled(function(tupledArg)
     {
      var data,_arg1,entries,x;
      data=tupledArg[0];
      _arg1=tupledArg[1];
      entries=data.responseData.feed.entries;
      x=OperatorIntrinsics.GetArraySlice(entries,{
       $:0
      },{
       $:1,
       $0:4
      });
      return Arrays.iter(function(x1)
      {
       var arg10;
       arg10=x1.link;
       return _ul_43_1.AppendI(Default.LI(List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("list-group-item-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg10),Default.Attr().NewAttr("target","_blank"),Default.Text(x1.title)]))])),Default.P(List.ofArray([Default.Text(x1.contentSnippet)]))])));
      },x);
     }));
     return Default.Div(List.ofArray([_ul_43_1]));
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
       return Client5.li(tupledArg[0],tupledArg[1],tupledArg[2],tupledArg[3],tupledArg[4]);
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
        return Concurrency.Bind(Remoting.Async("Sitelet:8",[]),function(arg101)
        {
         if(arg101.$==1)
          {
           Client5.displayQuestions(arg101.$0,elt);
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
      return Client5.main();
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
       return Client6.li(tupledArg[0],tupledArg[1],tupledArg[2]);
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
        return Concurrency.Bind(Remoting.Async("Sitelet:7",[]),function(arg101)
        {
         if(arg101.$==1)
          {
           Client6.dispalySnippets(arg101.$0,elt);
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
      return Client6.main();
     }
    })
   },
   StackOverflow:{
    Client:{
     main:function()
     {
      var x;
      x=Default.Div(Runtime.New(T,{
       $:0
      }));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:5",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         jQuery("#so-progress").fadeOut();
         Seq.iter(function(x2)
         {
          return elt.AppendI(Operators.add(Default.Div(List.ofArray([Default.Attr().Class("row data-row")])),x2));
         },Utils.split(2,Arrays.mapi(function(idx)
         {
          return function(q)
          {
           var cls,arg102,arg103;
           cls=idx%2===0?"col-md-5":"col-md-5 col-md-offset-1";
           arg102=q.ownerLink;
           arg103=q.ownerAvatar;
           return Operators.add(Default.Div(List.ofArray([Default.Attr().Class(cls)])),List.ofArray([Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",arg102),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("src",arg103),Default.Attr().Class("avatar")]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),Seq.toList(Seq.delay(function()
           {
            var arg104;
            arg104=q.link;
            return Seq.append([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading"),Default.Attr().NewAttr("style","word-break: break-word;")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg104),Default.Attr().NewAttr("target","_blank"),Default.Text(q.title)]))]))],Seq.delay(function()
            {
             return Seq.append([Default.P(List.ofArray([Default.Text(q.creationDate)]))],Seq.delay(function()
             {
              return Seq.append([Default.P(List.ofArray([Default.Text("Score: "),Default.Span(List.ofArray([Default.Attr().Class("badge"),Default.Text(Global.String(q.score))])),Default.Text(" Answers: "),Default.Span(List.ofArray([Default.Attr().Class("badge"),Default.Text(Global.String(q.answerCount))]))]))],Seq.delay(function()
              {
               return[Default.Div(Seq.toList(Seq.delay(function()
               {
                return Seq.map(function(x2)
                {
                 return Default.Span(List.ofArray([Default.Attr().Class("label label-primary"),Default.Text(x2)]));
                },q.tags);
               })))];
              }));
             }));
            }));
           })))]))]));
          };
         },arg101)));
         jQuery("[data-spy=\"scroll\"]").each(function()
         {
          return jQuery(this).scrollspy.call(null,"refresh");
         });
         return Concurrency.Return(null);
        });
       }));
      },x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client7.main();
     }
    })
   },
   Twitter:{
    Client:{
     main:function()
     {
      var x;
      x=Default.Div(List.ofArray([Default.Attr().Class("home-widget")]));
      Operators.OnAfterRender(function(elt)
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        var x1;
        x1=Remoting.Async("Sitelet:6",[]);
        return Concurrency.Bind(x1,function(arg101)
        {
         jQuery("#twitter-progress").fadeOut();
         Seq.iter(function(x2)
         {
          return elt.AppendI(Operators.add(Default.Div(List.ofArray([Default.Attr().Class("row data-row")])),x2));
         },Utils.split(2,Arrays.mapi(function(idx)
         {
          return function(tweet)
          {
           var p,cls,arg102,arg103,arg104,arg105;
           p=Default.P(Runtime.New(T,{
            $:0
           }));
           p.set_Html(tweet.statusAsHtml);
           cls=idx%2===0?"col-md-5":"col-md-5 col-md-offset-1";
           arg102="https://twitter.com/"+tweet.screenName;
           arg103=tweet.avatar;
           arg104="https://twitter.com/"+tweet.screenName;
           arg105=List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href","https://twitter.com/"+tweet.screenName+"/status/"+Global.String(tweet.id)),Default.Attr().NewAttr("target","_blank"),Default.Text(tweet.createdAt)]))]);
           return Operators.add(Default.Div(List.ofArray([Default.Attr().Class(cls)])),List.ofArray([Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.Attr().Class("media-left"),Default.Attr().NewAttr("href",arg102),Default.Attr().NewAttr("target","_blank")])),List.ofArray([Default.Img(List.ofArray([Default.Attr().NewAttr("src",arg103),Default.Attr().Class("avatar")]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("media-body")])),List.ofArray([Operators.add(Default.H4(List.ofArray([Default.Attr().Class("media-heading")])),List.ofArray([Default.A(List.ofArray([Default.Attr().NewAttr("href",arg104),Default.Attr().NewAttr("target","_blank"),Default.Text(tweet.screenName)])),Default.Span(List.ofArray([Default.Text(" ")])),Default.Tags().NewTag("small",arg105)])),p]))]))]));
          };
         },arg101)));
         jQuery("[data-spy=\"scroll\"]").each(function()
         {
          return jQuery(this).scrollspy.call(null,"refresh");
         });
         return Concurrency.Return(null);
        });
       }));
      },x);
      return x;
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client8.main();
     }
    })
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
      return Client9.main();
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
  Arrays=Runtime.Safe(WebSharper.Arrays);
  GitHubGists=Runtime.Safe(Website.GitHubGists);
  Client1=Runtime.Safe(GitHubGists.Client);
  Seq=Runtime.Safe(WebSharper.Seq);
  GitHubIssues=Runtime.Safe(Website.GitHubIssues);
  Client2=Runtime.Safe(GitHubIssues.Client);
  GitHubRepos=Runtime.Safe(Website.GitHubRepos);
  Client3=Runtime.Safe(GitHubRepos.Client);
  HTML5=Runtime.Safe(Default.HTML5);
  Login=Runtime.Safe(Website.Login);
  Client4=Runtime.Safe(Login.Client);
  window=Runtime.Safe(Global.window);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  jQuery=Runtime.Safe(Global.jQuery);
  News=Runtime.Safe(Website.News);
  OperatorIntrinsics=Runtime.Safe(WebSharper.OperatorIntrinsics);
  Questions=Runtime.Safe(Website.Questions);
  Client5=Runtime.Safe(Questions.Client);
  T=Runtime.Safe(List.T);
  Snippets=Runtime.Safe(Website.Snippets);
  Client6=Runtime.Safe(Snippets.Client);
  Utils=Runtime.Safe(Website.Utils);
  StackOverflow=Runtime.Safe(Website.StackOverflow);
  Client7=Runtime.Safe(StackOverflow.Client);
  Twitter=Runtime.Safe(Website.Twitter);
  Client8=Runtime.Safe(Twitter.Client);
  VideosAdmin=Runtime.Safe(Website.VideosAdmin);
  return Client9=Runtime.Safe(VideosAdmin.Client);
 });
 Runtime.OnLoad(function()
 {
  News.main();
  Client4.passInput();
  Client.addFormlet();
  return;
 });
}());
