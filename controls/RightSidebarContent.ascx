<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightSidebarContent.ascx.cs" Inherits="controls_RightSidebarContent" %>
			<div id="user-reviews">
                <script src="http://widgets.twimg.com/j/2/widget.js"></script>
<script>
    new TWTR.Widget({
        version: 2,
        type: 'profile',
        rpp: 4,
        interval: 6000,
        width: 220,
        height: 400,
        theme: {
            shell: {
                background: '#333333',
                color: '#ffffff'
            },
            tweets: {
                background: '#000000',
                color: '#ffffff',
                links: '#b10606'
            }
        },
        features: {
            scrollbar: false,
            loop: false,
            live: false,
            hashtags: true,
            timestamp: true,
            avatars: false,
            behavior: 'all'
        }
    }).render().setUser('GamerTheoryCom').start();
</script>
				
			</div>
