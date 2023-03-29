import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MenuContainerComponent } from './menu/menu-container/menu-container.component';
import { MenuComponentsComponent } from './menu/menu-components/menu-components.component';
import { AutomationContainerComponent } from './automation/automation-container/automation-container/automation-container.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuContainerComponent,
    MenuComponentsComponent,
    AutomationContainerComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
