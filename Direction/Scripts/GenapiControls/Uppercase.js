// Uppercase
function Uppercase()
{
	this.init = function (parentContainer) {
		$(parentContainer + " input[uppercase=\"true\"]").each(function () {

			// cet event pour lever le on change si c'est la bonne touche - on ne peut pas le faire dans le keypress car la lettre appuyée n'est pas encore écrite dans la textbox à ce moment - dans le keyup oui
			// le on change permet de mettre en lettre capitale
			$(this).keyup(function (e) {
				var key = e.which || e.keyCode || e.keyChar;
				// si on appuie sur Entrée > comportement par défaut
				if (key == 13) {
					return true;
				}
				if (key < 65 || (key >= 91 && key < 97) || key >= 129) {
					return false;
				}
				$(this).val($(this).val().toUpperCase());
			});

			// ce keypress pour empêcher la saisie si ce n'est pas dans a-Z (car on ne peut pas dans key up)
			$(this).keypress(function (e) {
				var key = e.which || e.keyCode || e.keyChar;
				// si on appuie sur Entrée > comportement par défaut
				if (key == 13) {
					return true;
				}
				if (key < 65 || (key >= 91 && key < 97) || key >= 129) {
					return false;
				}
			});
		});
	}
}