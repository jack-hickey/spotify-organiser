if (document.documentElement.dataset.flavour) {
	localStorage.setItem("flavour", document.documentElement.dataset.flavour.toLowerCase());
}

function setFlavour() {
	const flavour = (localStorage.getItem("flavour") ?? "default").toLowerCase(),
		target = document.querySelector("head");

	if (flavour !== "default" && target) {
		let flavourURL = document.documentElement.dataset.serverPath || location.origin;

		if (flavourURL.endsWith("/")) { flavourURL = flavourURL.substring(0, flavourURL.length - 1); }
		flavourURL = `${flavourURL}/css/flavours/${flavour}.css`;

		if (!document.querySelector(`#lnkFlavour[href^='${flavourURL}']`)) {
			target.appendChild(Object.assign(document.createElement("link"), {
				rel: "stylesheet",
				href: flavourURL,
				id: "lnkFlavour"
			}));
		}

		document.documentElement.dataset.flavour = flavour;
	}
}

setFlavour();

if (!window.frameElement) {
	const canChange = !["light", "dark"].includes(document.documentElement.dataset.theme);

	if (!document.documentElement.dataset.colour) {
		if (localStorage.getItem("colour")) {
			document.documentElement.dataset.colour = localStorage.getItem("colour");
		} else {
			document.documentElement.removeAttribute("data-colour");
		}
	}

	if (canChange && localStorage.getItem("site-theme") != null) {
		document.documentElement.dataset.theme = localStorage.getItem("site-theme");
	} else if (canChange && window.matchMedia?.('(prefers-color-scheme: dark)').matches) {
		document.documentElement.dataset.theme = "dark";

		localStorage.setItem("site-theme", "dark");
	} else if (canChange && window.matchMedia?.('(prefers-color-scheme: light)').matches) {
		document.documentElement.dataset.theme = "light";

		localStorage.setItem("site-theme", "light");
	}
} else {
	const parentTag = window.frameElement.closest("html");

	document.documentElement.dataset.colour ??= parentTag.dataset.colour;
	document.documentElement.dataset.theme ??= parentTag.dataset.theme;
}

// Collapse the sidebar if needed
const shouldCollapse = window.localStorage.getItem("collapseSidebar");

if (shouldCollapse === "true") {
	document.documentElement.classList.add("sidebar--collapsed");
} else if (shouldCollapse === "false") {
	document.documentElement.classList.remove("sidebar--collapsed");
}

// Try to use the correct code highlight file
function setCodeHighlighting() {
	const target = document.querySelector("link[href*='code-highlight']")

	if (target) {
		if (document.documentElement.dataset.theme === "dark") {
			target.href = target.href.replace("code-highlight.css", "code-highlight-dark.css");
		} else {
			target.href = target.href.replace("code-highlight-dark.css", "code-highlight.css");
		}
	}
}

setCodeHighlighting();