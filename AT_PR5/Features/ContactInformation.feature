Feature: ContactInformation

Contact info on contact page

@DataAccuracy
Scenario: CheckContactData
	Given user starts on the "contact" page
	Then contact information on the page matches following data:
	"""
	{
		"Email": "franciskscarynacr@gmail.com",
		"Phones": ["+370 68 771365", "+375 29 5781488"],
		"Socials": ["Facebook", "Telegram", "VK"]
	}
	"""
