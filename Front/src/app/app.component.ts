import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PessoasComponent } from './components/pessoas/pessoas.component';
import { TarefasComponent } from './components/tarefas/tarefas.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, PessoasComponent, TarefasComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'GerenciamentoDeTarefas';
}
