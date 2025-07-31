import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { Plus, Edit, Trash2, Users } from 'lucide-react';
import { autorService } from '@/lib/services';

export default function Autores() {
  const [autores, setAutores] = useState([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingAutor, setEditingAutor] = useState(null);
  const [formData, setFormData] = useState({ nome: '' });

  useEffect(() => {
    loadAutores();
  }, []);

  const loadAutores = async () => {
    try {
      setLoading(true);
      const response = await autorService.getAll();
      setAutores(response.data);
    } catch (error) {
      console.error('Erro ao carregar autores:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingAutor) {
        await autorService.update(editingAutor.id, { ...formData, id: editingAutor.id });
      } else {
        await autorService.create(formData);
      }
      setDialogOpen(false);
      setFormData({ nome: '' });
      setEditingAutor(null);
      loadAutores();
    } catch (error) {
      console.error('Erro ao salvar autor:', error);
    }
  };

  const handleEdit = (autor) => {
    setEditingAutor(autor);
    setFormData({ nome: autor.nome });
    setDialogOpen(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir este autor?')) {
      try {
        await autorService.delete(id);
        loadAutores();
      } catch (error) {
        console.error('Erro ao excluir autor:', error);
      }
    }
  };

  const resetForm = () => {
    setFormData({ nome: '' });
    setEditingAutor(null);
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-3">
          <Users className="h-8 w-8 text-green-600" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Autores</h1>
            <p className="text-gray-600">Gerencie os autores dos livros</p>
          </div>
        </div>
        <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
          <DialogTrigger asChild>
            <Button onClick={resetForm}>
              <Plus className="h-4 w-4 mr-2" />
              Novo Autor
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>
                {editingAutor ? 'Editar Autor' : 'Novo Autor'}
              </DialogTitle>
            </DialogHeader>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <Label htmlFor="nome">Nome</Label>
                <Input
                  id="nome"
                  value={formData.nome}
                  onChange={(e) => setFormData({ ...formData, nome: e.target.value })}
                  placeholder="Digite o nome do autor"
                  required
                />
              </div>
              <div className="flex justify-end space-x-2">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => setDialogOpen(false)}
                >
                  Cancelar
                </Button>
                <Button type="submit">
                  {editingAutor ? 'Atualizar' : 'Criar'}
                </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Lista de Autores</CardTitle>
        </CardHeader>
        <CardContent>
          {loading ? (
            <div className="text-center py-4">Carregando...</div>
          ) : (
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>ID</TableHead>
                  <TableHead>Nome</TableHead>
                  <TableHead className="text-right">Ações</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {autores.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={3} className="text-center py-4 text-gray-500">
                      Nenhum autor encontrado
                    </TableCell>
                  </TableRow>
                ) : (
                  autores.map((autor) => (
                    <TableRow key={autor.id}>
                      <TableCell>{autor.id}</TableCell>
                      <TableCell className="font-medium">{autor.nome}</TableCell>
                      <TableCell className="text-right">
                        <div className="flex justify-end space-x-2">
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleEdit(autor)}
                          >
                            <Edit className="h-4 w-4" />
                          </Button>
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleDelete(autor.id)}
                            className="text-red-600 hover:text-red-700"
                          >
                            <Trash2 className="h-4 w-4" />
                          </Button>
                        </div>
                      </TableCell>
                    </TableRow>
                  ))
                )}
              </TableBody>
            </Table>
          )}
        </CardContent>
      </Card>
    </div>
  );
}

