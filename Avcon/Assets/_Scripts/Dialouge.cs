using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialouge : MonoBehaviour {
	public Text dialougeText;

	public int phraseType;
	// 1 = MiniBoss-OfficeBFF
	// 2 = MainBoss-BAUSSS
	// 3 = Random Worker Chat 1
	// 4 = Random Worker Chat 2
	// 5 = Random Worker Chat 3
	// 6 = Random Worker Chat 4
	// 7 = Y2K Guy Dialogue

	string[] dialougeOptions = new string[] {"Sorry man gotta bail again!",
		"Not today bro, havin’ lunch with the boys from accounting",
		"You’re really over-exaggerating…",
		"Chill, man!",
		"I need you to fix this",
		"can you spot met his time",
		"I need to borrow some money",
		"You’re pretty brutal with the ladies, huh?",
		"You’re efficiency is down this month",
		"White, I’m going to need you in this Saturday",
		"Times are tough, a pay-cut may be in order",
		"you are replaceable",
		"Has that damn printer been fixed yet?",
		"Hmmm...Yeah, gonna need all this done by Lunch",
		"You disappoint me and this company",
		"A raise is out of the question",
		"You’re finally here?!",
		"Can you call I.T.? The coffee machine is broken!",
		"Hey are you the new intern? I need a decaf stat!",
		"The weekend can’t come soon enough.",
		"Alex in accounting really gets my gears going, if you know what I mean...",
		"Like I think the printer is still on the fritz!",
		"This computer is pretty much dead. You remember the number for I.T.?",
		"Like I’m so hungover I can barely tell what I’m doing… Oops.",
		"Please go away.",
		"Sometimes I wonder if I.T. does anything around here.",
		"Other people’s food in the fridge always tastes so much better than mine.",
		"Maintenance said I.T. wasn’t busy, what do they even do?",
		"How is that table still broken?",
		"Is I.T. here yet?",
		"Could you photocopy this?",
		"I.T. has to be the slowest dep by far...",
		"This coffee... It’s hot!!",
		"That last episode of Double Heights… Just wow!",
		"My computer’s broken… It’s not plugged in but that can’t be the problem.",
		"I heard the table broke thanks to steamy encounter, but that’s none of my business...",
		"No, for real, leave me alone.",
		"Wow this computer isn’t working… Are you I.T.?",
		"The lights are pretty dim…",
		"So if you’re I.T. I have a few questions...",
		"I can’t understand this wallpaper.",
		"What did you do to the power?",
		"Something seems wrong.",
		"WOOHOO! No work!!",
		"Eh… I.T.?",
		"This lighting is pretty romantic.",
		"My horoscope mentioned this!",
		"Debra from H.R. has put on a load of weight, but you didn’t hear that from me...",
		"What’s this computer disaster thing I keep hearing about?",
		"IS THE WORLD FOR REAL ENDING?!?!?!",
		"WHYYYYYY?!?!?!",
		"BUT I LOVE MY MICROWAVE!!!!",
		"ARGH!!!",
		"OH MY GOSH, I’M TOO SUCCESSFUL FOR THIS!",
		"YOU’RE SMART FIX IT!!",
		"WHY AREN'T YOU PANICKING?!?! COMPUTERS ARE LIKE YOUR JOB!!",
		"HOW CAN THIS HAPPEN TO ME?!",
		"ARGGHHHHHGHGH!!!!!!!!",
		"NOOOOO!!!!",
		"DO I KEEP MY JOB?!?!?!?",
		"CAN’T YOU FIX THIS?!?!",
		"WHERE IS THE GOVERNMENT WHEN WE NEED THEM?!",
		"THINK OF THE CHILDREN!!!",
		"The world is ending my friend. The computers will bring our demise."
							
	



		};
	// Use this for initialization
	void Start () {
		StartCoroutine ("changeText");
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator changeText () {
		while (true) {
			switch (phraseType) {
			default:
				break;
			case 1:
				dialougeText.text = dialougeOptions [Random.Range (1, 8)];
				break;
			case 2:
				dialougeText.text = dialougeOptions [Random.Range (9, 16)];
				break;
			case 3:
				dialougeText.text = dialougeOptions [Random.Range (17, 27)];
				break;
			case 4:
				dialougeText.text = dialougeOptions [Random.Range (28, 37)];
				break;
			case 5:
				dialougeText.text = dialougeOptions [Random.Range (38, 47)];
				break;
			case 6:
				dialougeText.text = dialougeOptions [Random.Range (48, 62)];
				break;
			case 7:
				dialougeText.text = dialougeOptions [63];
				break;
			}
			yield return new WaitForSeconds (5);
		}
	}
}
