import http from "k6/http";
import { check, sleep, group } from "k6";
import { TestConfig } from "./TestConfigK6.js";

let testConfig = new TestConfig();
export let options = testConfig.options;

export function setup() {
    let widgetIds = [1, 2, 3, 4, 5];

    return { widgetIds };
}

export default function (data) {
    group("Load Embedded HTML Test", function () {
        data.widgetIds.forEach(function (id) {
            let res = http.get(`http://localhost:5244/widgets/${id}/embedded`);

            check(res, {
                [`status was 200 for widget ${id}`]: (r) => r.status === 200,
            });
        });
    });

    sleep(1);
}
