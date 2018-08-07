using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvailabilityManager : MonoBehaviour {
    [SerializeField] private MissionDetails[] missionDets;
    [SerializeField] private BaseSwitcher baseSwitcher;

    private void Start() {
        Refresh();

        foreach (var det in missionDets) {
            det.SetAvailability(false);
        }
    }

    public void Refresh() {
        var ended = false;

        for (var i = 1; i <= 16; i++) {
            foreach (var det in missionDets) {
                if (det.MissionGroupID != i || det.AktRating != MissionDetails.Ratings.NOT_COMPLETED) continue;

                ended = true;
                break;
            }

            foreach (var det in missionDets) {
                if ((!ended && det.MissionGroupID == i + 1)) {
                    det.SetAvailability(true);
                }
            }

            if (ended) {
                break;
            }
        }
    }
}
