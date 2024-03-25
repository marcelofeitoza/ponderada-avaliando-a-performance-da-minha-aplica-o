class TestConfig {


    constructor() {
            
        this.params = {
            headers: {
                'Content-Type': 'application/json',
            },
            insecureSkipTLSVerify: true,
        };

        this.options = {
            stages: [
                { "duration": "1m", "target": 10 },
                // { "duration": "1m", "target": 0 },
                // { "duration": "5m", "target": 100 },
                // { "duration": "1m", "target": 0 },
                // { "duration": "15m", "target": 1000 },
                // { "duration": "1m", "target": 0 },
                // { "duration": "15m", "target": 10000 },
                // { "duration": "1m", "target": 0 },
                // { "duration": "30m", "target": 1000 },
                // { "duration": "1m", "target": 0 },
                // { "duration": "30m", "target": 10000 },
                // { "duration": "1m", "target": 0 }
            ],
        };
    }

    addStage(duration, target) {
        this.options.stages.push({ duration, target });
    }

}

export { TestConfig };

export let options = new TestConfig().options;
export default function() {
    new TestConfig().runTest(); 
}
