import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { DocumentationComponent } from './shared/documentation/documentation.component';
import { SavedComponent } from './shared/saved/saved.component';
import { HomeComponent } from './shared/home/home.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';

export const routes: Routes = [    
    {
        path: '', component: HomeComponent
    },
    {
        path: 'documentation', component: DocumentationComponent
    },
    {
        path: 'saved', component: SavedComponent
    },    
    {
        path: '**', component: NotFoundComponent
    }
    
];
