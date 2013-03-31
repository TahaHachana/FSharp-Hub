﻿namespace Website

module AddThis =

    open IntelliFactory.WebSharper
    open IntelliFactory.WebSharper.Html

    [<JavaScript>]
    let main() =
        let section = HTML5.Tags.Section []
        section.Html <-
            """<div class="addthis_toolbox addthis_default_style ">
               <a class="addthis_button_facebook_like" fb:like:layout="button_count"></a>
               <a class="addthis_button_tweet"></a>
               <a class="addthis_button_pinterest_pinit"></a>
               <a class="addthis_counter addthis_pill_style"></a>
               </div>
               <script type="text/javascript" src="http://s7.addthis.com/js/300/addthis_widget.js#pubid=ra-50af450141ce9366"></script>"""
        section

    type Control() =

        inherit Web.Control()

        [<JavaScript>]
        override __.Body = main() :> _