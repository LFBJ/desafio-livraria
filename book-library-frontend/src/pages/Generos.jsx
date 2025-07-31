import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { Plus, Edit, Trash2, Tag } from 'lucide-react';
import { generoService } from '@/lib/services';

export default function Generos() {
  const [generos, setGeneros] = useState([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingGenero, setEditingGenero] = useState(null);
  const [formData, setFormData] = useState({ nome: '' });

  useEffect(() => {
    loadGeneros();
  }, []);

  const loadGeneros = async () => {
    try {
      setLoading(true);
      const response = await generoService.getAll();
      setGeneros(response.data);
    } catch (error) {
      console.error('Erro ao carregar gêneros:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingGenero) {
        await generoService.update(editingGenero.id, { ...formData, id: editingGenero.id });
      } else {
        await generoService.create(formData);
      }
      setDialogOpen(false);
      setFormData({ nome: '' });
      setEditingGenero(null);
      loadGeneros();
    } catch (error) {
      console.error('Erro ao salvar gênero:', error);
    }
  };

  const handleEdit = (genero) => {
    setEditingGenero(genero);
    setFormData({ nome: genero.nome });
    setDialogOpen(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir este gênero?')) {
      try {
        await generoService.delete(id);
        loadGeneros();
      } catch (error) {
        console.error('Erro ao excluir gênero:', error);
      }
    }
  };

  const resetForm = () => {
    setFormData({ nome: '' });
    setEditingGenero(null);
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-3">
          <Tag className="h-8 w-8 text-purple-600" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Gêneros</h1>
            <p className="text-gray-600">Gerencie os gêneros literários</p>
          </div>
        </div>
        <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
          <DialogTrigger asChild>
            <Button onClick={resetForm}>
              <Plus className="h-4 w-4 mr-2" />
              Novo Gênero
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>
                {editingGenero ? 'Editar Gênero' : 'Novo Gênero'}
              </DialogTitle>
            </DialogHeader>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <Label htmlFor="nome">Nome</Label>
                <Input
                  id="nome"
                  value={formData.nome}
                  onChange={(e) => setFormData({ ...formData, nome: e.target.value })}
                  placeholder="Digite o nome do gênero"
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
                  {editingGenero ? 'Atualizar' : 'Criar'}
                </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Lista de Gêneros</CardTitle>
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
                {generos.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={3} className="text-center py-4 text-gray-500">
                      Nenhum gênero encontrado
                    </TableCell>
                  </TableRow>
                ) : (
                  generos.map((genero) => (
                    <TableRow key={genero.id}>
                      <TableCell>{genero.id}</TableCell>
                      <TableCell className="font-medium">{genero.nome}</TableCell>
                      <TableCell className="text-right">
                        <div className="flex justify-end space-x-2">
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleEdit(genero)}
                          >
                            <Edit className="h-4 w-4" />
                          </Button>
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleDelete(genero.id)}
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

