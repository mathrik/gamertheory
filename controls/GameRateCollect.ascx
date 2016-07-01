<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GameRateCollect.ascx.cs" Inherits="controls_GameRateCollect" %>
<script type="text/javascript">
        <!--

		function removeFromCollection(gameID) {
			$.get('/ajax/remove-from-user-collection.aspx', { objectID: gameID, objecttype: 1 }, function (data) {
				if (data == "success") {
					$("#gamecollection" + gameID).html("Game Removed.");
				} else {
					alert("An error occurred: " + data);
				}
			});
		}

		function addToCollection(gameID) {
			$.get('/ajax/update-user-collection.aspx', { objectID: gameID, objecttype: 1 }, function (data) {
				if (data == "success") {
					$("#gamecollection" + gameID).html("Game Added.");
				} else {
					alert("An error occurred: " + data);
				}
			});
		}

		//var arrRatingText = Array('Not Rated', 'Excruciating', 'Awful', 'Not Bad', 'Recommended', 'Must Play');
		var RANKING_LIST = new Array();
		<%=jsRankingArray %>

		function getRating(objectID) {
			if (isNaN(RANKING_LIST[objectID])) {
				return 0;
			} else {
				return RANKING_LIST[objectID];
			}
		}

		function previewRating(n, objectID) {
			$("#rating" + objectID).css("background-image", "url(/images/rating" + n + ".png)");
			//$("#ratingText" + objectID).text(arrRatingText[n]);
		}

		function setRating(objectID) {
			$("#rating" + objectID).css("background-image", "url(/images/rating" + getRating(objectID) + ".png)");
			//$("#ratingText" + objectID).text(arrRatingText[getRating(objectID)]);
		}

		function addRating(ratingVal, objectID) {
			$.get("/ajax/update-user-rating.aspx", { object: objectID, objecttype: 1, rating: ratingVal },
					function (data) {
						if (data == "success") {
							$("#rating" + objectID).css("background-image", "url(/images/rating" + ratingVal + ".png)");
							RANKING_LIST[objectID] = ratingVal;
							//$("#ratingText" + objectID).html("Thanks for rating!");
						} else {
							alert("Ranking failed: " + data);
						}
					}
				);
			return false;
		}

		$(document).ready( function () {
			<%=jsOnload %>
		});
        //-->
        </script>	