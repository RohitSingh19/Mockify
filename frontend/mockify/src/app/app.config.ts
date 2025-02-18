import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { NU_MONACO_EDITOR_CONFIG } from '@ng-util/monaco-editor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    {
      provide: NU_MONACO_EDITOR_CONFIG,
      useValue: {
        defaultOptions: { scrollBeyondLastLine: false },
        monacoLoad: () => {
          const uri = monaco.Uri.parse('a://b/foo.json');
          monaco.languages.json.jsonDefaults.setDiagnosticsOptions({
            validate: true,
            schemas: [
              {
                uri: 'http://myserver/foo-schema.json',
                fileMatch: [uri.toString()],
                schema: {
                  type: 'object',
                  properties: {
                    p1: {
                      enum: ['v1', 'v2'],
                    },
                    p2: {
                      $ref: 'http://myserver/bar-schema.json',
                    },
                  },
                },
              },
              {
                uri: 'http://myserver/bar-schema.json',
                fileMatch: [uri.toString()],
                schema: {
                  type: 'object',
                  properties: {
                    q1: {
                      enum: ['x1', 'x2'],
                    },
                  },
                },
              },
            ],
          });
        },
      },
    },
  ],
};
