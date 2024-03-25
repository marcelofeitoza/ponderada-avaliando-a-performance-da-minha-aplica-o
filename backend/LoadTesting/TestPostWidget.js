import http from 'k6/http';
import { check, sleep } from 'k6';
import { TestConfig } from './TestConfigK6.js';

// Create an instance of TestConfig
let testConfig = new TestConfig();
export let options = testConfig.options;

export default function () {
    const url = 'http://localhost:5244/widgets';

    // Post widget
    let payload = JSON.stringify({
        title: 'title',
        link: 'link',
        question: 'question',
    });

    let postWidgetResponse = http.post(url, payload,  testConfig.params);
    check(postWidgetResponse, { 'status was 201': (r) => r.status === 201 });
    sleep(1);
}