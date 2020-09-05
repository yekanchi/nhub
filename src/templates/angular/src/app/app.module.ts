import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { GridModule } from '@syncfusion/ej2-angular-grids';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/appComponent/app.component';
import { PiListComponent } from './components/bpa/pi-list/pi-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    PiListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GridModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
