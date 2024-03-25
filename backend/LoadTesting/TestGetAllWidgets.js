import http from 'k6/http';
import { check, sleep } from 'k6';
import { TestConfig } from './TestConfigK6.js';

// Create an instance of TestConfig
let testConfig = new TestConfig();
export let options = testConfig.options;

export default function () {
    const url = 'http://localhost:5244/widgets';

    // Get all widgets
    let getWidgetsResponse = http.get(url);
    check(getWidgetsResponse, { 'status was 200': (r) => r.status === 200 });
    sleep(1);
}