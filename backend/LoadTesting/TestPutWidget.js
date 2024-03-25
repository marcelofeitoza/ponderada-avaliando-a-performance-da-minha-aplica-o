import http from 'k6/http';
import { check, sleep } from 'k6';
import { TestConfig } from './TestConfigK6.js';

// Create an instance of TestConfig
let testConfig = new TestConfig();
export let options = testConfig.options;

export default function () {
    const url = 'http://localhost:5244/widgets';

    // Put widgets
    const id = 1; // O ID do widget que você deseja atualizar
    const payload = JSON.stringify({
        Title: "New Widgets",
        Link: "link",
        Question: "o que achou da plataforma?",
        Color: "blue"
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = http.put(`${url}/${id}`, payload, params);
    check(response, {
        'is status 200': (r) => r.status === 200,
        'is not error': (r) => !r.body.error,
    });
}