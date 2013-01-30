/*** POPUP ***/
function PopupClass()
{
	this.init = function (frkHandler) {
		// Destruction des anciens dialogs
		$("#dialog:ui-dialog").dialog("destroy");

		$("[actiontype=OpenPopup]").click(function (event) {

			// On stop les événements pouvant être lancés par les autres liens de la page
			event.stopImmediatePropagation();

			var aLink = $(this).find("a");
			var href = aLink.attr("href");

			$.get(
                    href,
                    function (htmlResult) {
                    	$("#PopupContainer").remove();
                    	$("#PopupHidden").append(htmlResult);
                    	$("#PopupContainer").dialog({
                    		resizable: false,
                    		draggable: true,
                    		width: 1020,
                    		modal: true,
                    		close: function (ev, ui) {
                    			// Le popup contient un formulaire?
                    			if ($(this).find('form').length > 0) {
                    				// On retire le popup de confirmation pour quitter la page
                    				window.onbeforeunload = null;
                    			}
                    		}
                    	});

                    	var title = $("#PopupContainer .popuptitle").get(0);

                    	if (title) {
                    		$("#PopupContainer").dialog("option", "title", title.innerHTML);
                    		$(title).remove();
                    	}

                    	if (frkHandler) {
                    		frkHandler.reinitControls("#PopupContainer");
                    		frkHandler.globalHandler.initPopupAjaxFormPost("#PopupContainer", frkHandler);
                    	}

                    	// Calcul et set de la hauteur du popup
                    	var popupHeight = $(window).height() - 100;

                    	if (popupHeight > $("#PopupContainer").height() + 110) {
                    		popupHeight = $("#PopupContainer").height() + 110;
                    	}

                    	$("#PopupContainer").dialog({ height: popupHeight });
                    	$("#PopupContainer").dialog({ position: 'center' });
                    }
                );
			return false;
		});
	}
}