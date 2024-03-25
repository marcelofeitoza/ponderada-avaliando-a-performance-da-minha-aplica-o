import http from 'k6/http';
import { check, sleep } from 'k6';
import { TestConfig } from './TestConfigK6.js';

// Create an instance of TestConfig
let testConfig = new TestConfig();
export let options = testConfig.options;

export default function () {
    const url = 'http://localhost:5244/widgets';

    // Post NPS
    let npsPayload = JSON.stringify({
        widgetId: 'widgetId',
        answer: 'answer',
        rating: 'rating'
    });

    let postNpsResponse = http.post(`${url}/1/nps`, npsPayload, testConfig.params);
    check(postNpsResponse, { 'status was 201': (r) => r.status === 201 });
}