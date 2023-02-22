
$(function () {
    var nwidth, nheight;
    $(".menu_item").click(function () {
            alert(new String(this.innerText).replace('      ',''));
    });
		$("#accordion").accordion({
			collapsible: true,
			active: false
			});
			
		 $("#close").click(function() {
			  $( ".tree_panel" ).toggleClass( "panel_close", 200 );
			  $(".map_content").toggleClass("expand_map", 200);

			  if ($(".map_content").attr('class') == 'map_content float_left expand_map') {
			      if ($.browser.msie) { //ie
			          nwidth = document.getElementsByTagName("body")[0].clientWidth - 460;
			      }
			      else {
			          nwidth = document.getElementsByTagName("body")[0].clientWidth - 357;
			      }
			      nheight = document.getElementsByTagName("body")[0].clientHeight - 111;
			  }
			  else {
			      if ($.browser.msie) { //ie
			          nwidth = document.getElementsByTagName("body")[0].clientWidth - 84;
			          
			      }
			      else {
			          nwidth = document.getElementsByTagName("body")[0].clientWidth - 55;
			      }
			      nheight = document.getElementsByTagName("body")[0].clientHeight - 111;
			  }
			  $("#body").css("overflow", "hidden");
			  $("#map").css("position", "absolute");
			  $("#map").css("overflow", "auto");
			  $("#map").css("height", nheight);
			  $("#map").css("width", nwidth);
			  $("#map").css("z-index", "99")
			  $("#map").css("top", "30px");
			  $("#map").css("left", "1px");
			  google.maps.event.trigger(map, "resize");
			  return false;
     	  });
});