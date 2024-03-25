import http from 'k6/http';
import { check, sleep } from 'k6';
import { TestConfig } from './TestConfigK6.js';

// Create an instance of TestConfig
let testConfig = new TestConfig();
export let options = testConfig.options;

export default function () {
    const url = 'http://localhost:5244/widgets';

    // Get widget by id
    let getWidgetResponse = http.get(`${url}/1`);
    check(getWidgetResponse, { 'status was 200': (r) => r.status === 200 });
    sleep(1);
}