Feature: Localization

Site localization on different languages

@UserAccessibility
Scenario: CanguageChange
	Given user starts on the "home" page 
	When user changes language through drop down language list
	Then opens version of main page on coresponding language with matchin URL
