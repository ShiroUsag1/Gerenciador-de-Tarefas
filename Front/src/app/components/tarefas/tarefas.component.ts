import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/apiService/api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tarefas.component.html',
  styleUrls: ['./tarefas.component.css'],
})
export class TarefasComponent implements OnInit {
  tarefas: any[] = [];
  blank: any = {
    titulo: '',
    descricao: '',
    status: 'pendente',
  };
  novaTarefa: any;
  buttonName: string = 'Adicionar';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadTarefas();
    this.novaTarefa = this.blank;
  }

  definirDados(dados: any) {
    this.novaTarefa = dados;
    this.buttonName = 'Salvar';
    const elementoDestino = document.getElementById('formTarefa');
    if (elementoDestino) {
      elementoDestino.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }

  checkAddorUpdate() {
    if (this.buttonName == 'Salvar') {
      this.updateTarefa(this.novaTarefa.tarefaId, this.novaTarefa);
    } else {
      this.addTarefa();
    }
  }

  loadTarefas() {
    this.apiService.getTarefas().subscribe((data) => {
      this.tarefas = data;
    });
  }

  addTarefa() {
    this.apiService.addTarefa(this.novaTarefa).subscribe((data) => {
      this.tarefas.push(data);
      this.novaTarefa = { titulo: '', descricao: '', status: 'pendente' };
    });
  }

  deleteTarefa(id: number) {
    this.apiService.deleteTarefa(id).subscribe({
      next: () => {
        this.tarefas = this.tarefas.filter((tarefa) => tarefa.id !== id);
        window.location.reload();
      },
      error: (err) => {
        const errorMessage = err.error.message;
        alert(errorMessage);
      },
    });
  }

  updateTarefa(id: number, tarefa: any) {
    this.apiService.updateTarefa(id, tarefa).subscribe({
      next: () => {
        this.tarefas = this.tarefas.filter((tarefa) => tarefa.id !== id);
        alert('Tarefa atualizada com sucesso!');
        this.novaTarefa = this.blank;
      },
      error: (err) => {
        const errorMessage = err.error.message;
        alert(errorMessage);
      },
    });
  }
}
