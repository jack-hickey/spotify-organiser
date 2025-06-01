window.btnOrganise?.addEventListener("click", () => {
	Ajax.Post(Razor.GetHandlerURL("Organise"), {
		success: {
			ok: response => {
				Dialog.ShowSuccess("Success!", "Nice! Your songs on Spotify have been organised into playlists based on their genre.");
			}
		}
	})
});