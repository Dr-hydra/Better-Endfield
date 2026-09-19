import unittest
from convert_reviewed_draws import canonical_ini, canonical_shader, validate_source, relative


class MemorySource:
    def __init__(self, files): self.files = files
    def read_exact(self, name): return self.files[name].encode()


class ReviewedSourceTests(unittest.TestCase):
    def setUp(self):
        self.files = {'model.ini': '[Constants]\nglobal persist $swap=0\n[Draw]\nrun = CustomShaderCopy\n',
                      'copy.hlsl': 'void main(){ dst[0] = src[0]; }'}
        self.recipe = {'ini': 'model.ini', 'source_program': canonical_ini(self.files['model.ini']),
                       'shader_programs': {'copy.hlsl': canonical_shader(self.files['copy.hlsl'])}}

    def test_comment_changes_allowed_but_program_changes_rejected(self):
        files = dict(self.files); files['model.ini'] += '; documentation only\n'
        validate_source(MemorySource(files),self.recipe)
        for mutation in ('\n[Other]\nrun = CustomShaderUnknown\n',):
            files['model.ini'] += mutation
            with self.assertRaisesRegex(ValueError,'INI differs'):
                validate_source(MemorySource(files),self.recipe)

    def test_modified_shader_and_default_are_rejected(self):
        files = dict(self.files); files['copy.hlsl'] = files['copy.hlsl'].replace('src[0]','src[1]')
        with self.assertRaisesRegex(ValueError,'shader differs'):
            validate_source(MemorySource(files),self.recipe)
        files = dict(self.files); files['model.ini'] = files['model.ini'].replace('$swap=0','$swap=1')
        with self.assertRaisesRegex(ValueError,'INI differs'):
            validate_source(MemorySource(files),self.recipe)

    def test_unsafe_resource_paths_rejected(self):
        for name in ('../copy.hlsl','C:/copy.hlsl','/copy.hlsl','..\\copy.hlsl'):
            with self.assertRaises(ValueError): relative(name)


if __name__ == '__main__': unittest.main()
