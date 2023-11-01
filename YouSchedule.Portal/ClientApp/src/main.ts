import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

import { ChartsModule } from 'ng2-charts';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

/*
import '../scss/styles.scss';
import * as mdb from 'mdb-ui-kit';
//window.mdb = mdb;

import 'mdb-ui-kit/mdb-ui-kit/src/scss/mdb.free.scss';
*/

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
