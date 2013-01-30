/*** GLOBAL ***/
function GlobalClass() {
    this.initFormEvent = function (parentContainer) {
        var self = this;
        // Loading panel: Empecher l'utilisateur de cliquer deux fois sur le bouton save
        $(parentContainer + ' form').submit(function () {
            self.showLoadingProgress();

            if ($(this).valid() == false) {
                self.removeLoadingProgress();
            }

            window.onbeforeunload = null;
        });

        // Empecher de quitter une form en cours d'édition
        // commenté car c'est ingérable mais si on veut le remettre ... on garde le code au cas où (et non, on ne le garde pas dans l'historique de TFS mais ici ^^
        //		if ($(parentContainer + ' form').length > 0)
        //		{
        //			window.onbeforeunload = function () { return "You work will be lost."; };
        //		}
    }

    this.initRequiredField = function (parentContainer) {
        $(parentContainer + " [data-val-required]").each(function () {
            $(this).parent().addClass("required");
        });
    }

    this.initPopupAjaxFormPost = function (parentContainer, frkHandler) {
        $(parentContainer + ' form').submit(function () {
            if ($(this).valid() == true) {
                var form = $(this);
                var action = $(this).attr("action");

                var serializedForm = form.serialize();

                $.ajax({
                    url: action,
                    type: 'POST',
                    data: serializedForm,
                    success: function (result, textStatus, jqXHR) {
                        // Redirection?
                        if (jqXHR.responseText.indexOf("window.location=") != 0) {
                            form[0].innerHTML = result;
                            $("#loadingProgress").dialog().remove();
                            frkHandler.reinitControls(parentContainer);
                        }
                    }
                });
            }
            return false;
        });
    }

    this.showLoadingProgress = function () {
        $("#loadingProgress").dialog({
            modal: true
        });

        $("#loadingProgress").siblings('div.ui-dialog-titlebar').remove();
    }

    this.removeLoadingProgress = function () {
        $("#loadingProgress").dialog('close');
    }

    this.initControls = function (parentContainer) {
        $(parentContainer + " [controltype=\"ajaxAction\"]").each(function () {
            $(this).click(function (event) {
                var hrefAction = $(this).attr("actionUrl");
                if ($(this).attr('type') == 'checkbox') {
                    hrefAction += "?isChecked=" + $(this).is(':checked');
                }
//                gaFrk.globalHandler.showLoadingProgress();
                $.ajax({
                    url: hrefAction,
                    type: 'POST',
                    success: function (result, textStatus, jqXHR) {
                        // Redirection?
                        if (jqXHR.responseText.indexOf("window.location=") != 0) {
//                            gaFrk.globalHandler.removeLoadingProgress();
                        }
                    }
                });
                return false;
            });
        });

        // affiche la loading progress pour tous les liens
        $(parentContainer + " a").not("[controltype=\"downloadablefile\"]").add(parentContainer + " :button").click(function () {
  //          gaFrk.globalHandler.showLoadingProgress();
        });
    }
}