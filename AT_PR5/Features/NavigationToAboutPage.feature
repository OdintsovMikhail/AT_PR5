Feature: NavigationToAboutPage

Simple navigation on about page

@Navigation
Scenario: Navigate to about page
	Given user starts on the "home" page
	When user clicks on "About" on navigation bar
	Then about page opens, containing "About" in page and content headers
