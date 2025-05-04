Feature: Search

Search by input functionality

@Search
Scenario: SearchStudyPrograms
	Given user starts on the "home" page
	When user uses search on navigation bar to find 'study programs'
	Then search page opens with coresponding search results and url
